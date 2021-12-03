using Godot;
using System;
using System.Collections.Generic;
using System.IO.Ports;

public class MainWindow : Control
{
	private SerialPort port;
	private string[] availablePorts;
	
	private OptionButton portSelector;
	private Button connectButton;
	private ScrollContainer graphContainer;
	private Control graphBox;
	private Line2D graph;
	
	private Label voltageLabel;
	
	private int loggedPoints = 0;
	
	private void WriteToPort(String message) {
		port.WriteLine(message);
	}
	
	private void InitialisePort() {
		string selectedPort = portSelector.GetItemText(portSelector.GetSelectedId());
		int baudrate = 9600;
		port = new SerialPort(selectedPort, baudrate, Parity.None, 8, StopBits.One);
		port.Open();
	}
	
	public override void _Ready() {
		// Init UI elements
		portSelector = (OptionButton) GetNode("SerialPortSelector");
		connectButton = (Button) GetNode("ConnectButton");
		connectButton.Connect("pressed", this, nameof(InitialisePort));
		
		graphContainer = (ScrollContainer) GetNode("ScrollContainer");
		graphBox = (Control) GetNode("ScrollContainer/Container");
		graph = (Line2D) GetNode("ScrollContainer/Container/Line2D");
		
		voltageLabel = (Label) GetNode("VoltageLabel");
		
		availablePorts = SerialPort.GetPortNames();
		foreach(string portname in availablePorts) {
			System.Console.WriteLine(portname);
			portSelector.AddItem(portname);
		}
	}

	public override void _Process(float delta) {
		graphContainer.GetHScrollbar().Value = graphContainer.GetHScrollbar().MaxValue;
		
		try {
			port.DiscardInBuffer();
			String temp = port.ReadLine();
			temp = port.ReadLine();
			if(temp.Length >= 4) {
				voltageLabel.Text = temp + "V";
				float voltage = float.Parse(temp);
				graph.AddPoint(new Vector2(loggedPoints * 5, -voltage * 10));
				loggedPoints ++;
				Vector2 size = graphBox.RectMinSize;
				size[0] += 5;
				graphBox.RectMinSize = size;
			}
		} catch(Exception) { }
	}
}
