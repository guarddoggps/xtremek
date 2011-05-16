
using System;
using AlarmasABC.DAL.Insert;

namespace AlarmasABC.BLL
{
	
	public class UnitCommand
	{
		public enum Command
		{
			POSITION_POLL,
			SET_UPDATE_FREQUENCY,
			
			TURN_OFF_VEHICLE,
			TURN_ON_VEHICLE,
			
			STOP_DELAY_AFTER_MOVING,
			STOP_DELAY_AT_SAME_PLACE
		}
			
		public UnitCommand()
		{
		}
		
		private int getFirstByte(int num)
		{
			return num % 256;
		}
		
		private int getSecondByte(int num)
		{
			return num / 256;
		}
		
		public void SendUnitCommand(int deviceID, string command)
		{
			UnitCommandInsert unitCmdInsert = new UnitCommandInsert();
			unitCmdInsert.DeviceID = deviceID;
			unitCmdInsert.MsgBody = command;
			
			unitCmdInsert.invoke();		
		}
		
		public void SendUnitCommand(int deviceID, Command command, string parameter)
		{
			bool hasInputParameter = false;
			string commandCode = "";
			string commandParameter = "";
			string msgBody = "";
			
			switch (command)
			{
			case Command.POSITION_POLL:
				commandCode = "0";
				break;
			case Command.SET_UPDATE_FREQUENCY:
				hasInputParameter = true;
				commandCode = "1";
				break;
			case Command.STOP_DELAY_AFTER_MOVING:
				hasInputParameter = true;
				commandCode = "67";
				break;
			case Command.STOP_DELAY_AT_SAME_PLACE:
				hasInputParameter = true;
				commandCode = "68";
				break;
			case Command.TURN_OFF_VEHICLE:
				commandCode = "36";
				commandParameter = "3";
				break;
			case Command.TURN_ON_VEHICLE:
				commandCode = "36";
				commandParameter = "131";
				break;
			};
			
			if (hasInputParameter)
			{
				int param = Convert.ToInt32(parameter);
				msgBody = commandCode + "," + getFirstByte(param) + "," + getSecondByte(param);
			}
			else 
			{
				msgBody = commandCode + "," + commandParameter;
			}
			
			SendUnitCommand(deviceID, msgBody);
			
			// Execute the insert query
			/*UnitCommandInsert unitCmdInsert = new UnitCommandInsert();
			unitCmdInsert.DeviceID = deviceID;
			unitCmdInsert.MsgBody = msgBody;
			
			unitCmdInsert.invoke();		*/	
		}
	}
}
