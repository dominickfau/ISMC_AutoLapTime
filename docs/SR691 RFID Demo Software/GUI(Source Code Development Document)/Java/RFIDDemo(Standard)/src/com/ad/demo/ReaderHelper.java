package com.ad.demo;

import com.ad.comm.CommStatus;
import com.ad.comm.CommType;
import com.ad.comm.StatusEventArg;
import com.ad.rfid.OnProtocolListener;
import com.ad.rfid.ProtocolEventArg;
import com.ad.rfid.ProtocolStandard;
import com.ad.rfid.passive.PassiveBaseParameters;
import com.ad.rfid.passive.PassiveCommand;
import com.ad.rfid.passive.PassiveRcp;
import com.ad.utils.ConverterUtil;

public class ReaderHelper {

	//1-0	ȷ��ͨѶ��ʽ
	private static int mCommType = CommType.USB;
	private static String mHost = "";
	private static int mPort = 1;
	private static PassiveBaseParameters mBaseParameter = new PassiveBaseParameters();
	//2-0	����豸����ͨѶ�� ,�������Ϊ�첽ͨ��
	private static PassiveRcp mADRcp = new PassiveRcp();

	//3-0	����豸���ݴ��ݽ�����ص�����
	private static void init(){
		mADRcp.setOnProtocolListener(onProtocolListener);		
	}

	private static OnProtocolListener onProtocolListener = new OnProtocolListener() {

		@Override
		public void onReceived(Object obj, ProtocolEventArg event) {
			// TODO Auto-generated method stub
			/*
            byte[] reciveBytes = protocolEventArg.getData();
			System.out.println("recv data Len1: "+reciveBytes.length+" Recv data: "+ConverterUtil.ByteArrayToHexString(reciveBytes));*/	
			//5-0
			func(event);
		}

		@Override
		public void onStatus(Object arg0, StatusEventArg arg1) {
			// TODO Auto-generated method stub		
			System.out.println("Object1: "+arg0.toString()+" status: "+CommStatus.format(arg1.getStatus())+" msg: "+arg1.getMsg());	
			//4-0
			statuss(arg1);
		}
		
	};

	//4-0	����״̬����
	private static void statuss(StatusEventArg arg1){
		switch(arg1.getStatus()){
		case CommStatus.CONNECTING:
			System.out.println(
					" status: "+CommStatus.format(arg1.getStatus())+
					" msg: "+arg1.getMsg());
			break;
		case CommStatus.CONNECT_OK:
			System.out.println(
					" status: "+CommStatus.format(arg1.getStatus())+
					" msg: "+arg1.getMsg());
			break;
		case CommStatus.CONNECT_EXCEPT:
			System.out.println(
					" status: "+CommStatus.format(arg1.getStatus())+
					" msg: "+arg1.getMsg());
			break;
		case CommStatus.DISCONNECT_OK:
			System.out.println(
					" status: "+CommStatus.format(arg1.getStatus())+
					" msg: "+arg1.getMsg());
			break;
		case CommStatus.DISCONNECT_EXCEPT:
			System.out.println(
					" status: "+CommStatus.format(arg1.getStatus())+
					" msg: "+arg1.getMsg());
			break;
		case CommStatus.SEND_EXCEPT:
			System.out.println(
					" status: "+CommStatus.format(arg1.getStatus())+
					" msg: "+arg1.getMsg());
			break;
		case CommStatus.RECEIVE_EXCEPT:
			System.out.println(
					" status: "+CommStatus.format(arg1.getStatus())+
					" msg: "+arg1.getMsg());
			break;
		}
	}
	
	//5-0 	�������ݽ�������
	private static void func(ProtocolEventArg event){
		//��׼ͨѶЭ�����
		ProtocolStandard psData = event.getProtocolStandard();
		
		switch(psData.Code){
        case PassiveRcp.RCP_CMD_INFO:
        	System.out.println("Response Information [Type: "+ mADRcp.getType()+" "+
                            "Mode: "+ mADRcp.getMode()+" "+
                            "Version: "+ mADRcp.getVersion()+" "+
                            "Address: "+ mADRcp.getAddress()+"]");
            break;
        case PassiveRcp.RCP_CMD_PARA:
            if (psData.Length > 0 && psData.Type == 0) {
            	mBaseParameter.AddRange(psData.Payload);
                String strpara = ConverterUtil.ByteArrayToHexString(psData.Payload, 0, psData.Length);
                System.out.println("Response parameters ["+strpara+"]");
            }else if (psData.Type == 0)
            {
            	System.out.println("Set parameters succeed!");
            }else{
            	System.out.println("Action parameters fail!");
            }
            break;
        case PassiveRcp.RCP_CMD_ISO6B_IDEN:
            if (psData.Length > 0 && psData.Type == 0x32) {
                String epcStr = ConverterUtil.ByteArrayToHexString(psData.Payload, 1, psData.Length - 1);
            	System.out.println("Auto Send 6B TID No.(hex):" + epcStr);
            }
            else if (psData.Length > 0 && psData.Type == 0x00) {
                String epcStr = ConverterUtil.ByteArrayToHexString(psData.Payload, 1, psData.Length - 1);
            	System.out.println("Response 6B TID No.(hex):" + epcStr);
            }
            else{
            	System.out.println("Unidentified 6B TID tag!");
            }
            break;
        case PassiveRcp.RCP_CMD_ISO6B_RW:
            if (psData.Length > 0 && psData.Type == 0) {
                final String strread = ConverterUtil.ByteArrayToHexString(psData.Payload,1,psData.Length-1);
            	System.out.println("Response 6B data(hex):" + strread);
            }else if(psData.Length == 0 && psData.Type == 0) {
            	System.out.println("Write 6B succeed!");
            }else{
            	System.out.println("Action 6B fail!");
            }
            break;
        case PassiveRcp.RCP_CMD_EPC_IDEN:
            if (psData.Length > 0 && psData.Type == 0x32) {
                String epcStr = ConverterUtil.ByteArrayToHexString(psData.Payload, 1, psData.Length - 1);
            	System.out.println("Auto Send Epc No.(hex):" + epcStr);
            }
            else if (psData.Length > 0 && psData.Type == 0x00) {
                String epcStr = ConverterUtil.ByteArrayToHexString(psData.Payload, 1, psData.Length - 1);
            	System.out.println("Response Epc No.(hex):" + epcStr);
            }
            else{
            	System.out.println("Unidentified Epc tag!");
            }
            break;
        case PassiveRcp.RCP_CMD_EPC_RW:
            if (psData.Length > 0 && psData.Type == 0) {
                final String strread = ConverterUtil.ByteArrayToHexString(psData.Payload,1,psData.Length-1);
            	System.out.println("Response Epc data(hex):" + strread);

            }else if(psData.Length == 0 && psData.Type == 0) {
            	System.out.println("Write Epc succeed!");
            }else{
            	System.out.println("Action Epc fail!");
            }
            break;
		}
	}
	
	//6-0	ͨѶ�������豸,����ͨѶ��ʽ��ͬѡ��ͬ����
	private static void choiceCommType(){
		switch(mCommType)
		{
		case CommType.SERIAL:
			mHost = "COM1";
			mPort = 9600;
			break;
		case CommType.NET:
			mHost = "192.168.2.116";
			mPort = 49152;
			break;
		case CommType.USB:
			mHost = "0811";
			mPort = 1;
			break;
		default:
			mHost = "COM1";
			mPort = 9600;
			break;
		}
	}
	//7-0	�����豸,���ؽ����ʹ��onProtocolListener
	private static void connect(){
		mADRcp.connect(mHost,mPort, mCommType);
	}
	
	//8-0	�������ݴ��ݽ�����̬���ݷ��ʹ����,�������Ϊ�첽ͨ��,���ؽ����ʹ��onProtocolListener
	private static void runs() throws InterruptedException{
		//8-1	��ȡ�豸������Ϣ
		PassiveCommand.GetInformation(mADRcp);
		Thread.sleep(1000);
		
		//8-2  	��ȡ�豸��������
		//8-2	get base config
		PassiveCommand.GetConfig(mADRcp);
		Thread.sleep(1000);
		
		//8-3  	�����豸��������
		//8-3 	set base config
		PassiveCommand.SetConfig(mADRcp,mBaseParameter.ToArray());
		Thread.sleep(1000);
		
		//8-4 	 ʶ��6C��
		//8-4  	Identify 6C
		PassiveCommand.Identify6C(mADRcp);
		Thread.sleep(1000);

		//8-5  	��ȡ6C������
		//8-5 	Read 6C
		int iMem = 1;		//Target memory bank; 0 Reserved, 1 EPC, 2 TID, 3 User 
		int iStart = 2;		//Starting Address word pointer of memory bank
		int iLength = 4;	//Data Length of read (byte Count)
		PassiveCommand.Read6C(mADRcp, iMem, iStart, iLength);
		Thread.sleep(1000);
		
		//8-6  	д��6C������
		//8-6 	Write 6C
		iMem = 1;
		iStart = 2;
		iLength = 4;
		byte[] iData = new byte[]{0x01,0x02,0x03,0x04};
		PassiveCommand.Write6C(mADRcp, iMem, iStart, iLength,iData);
		Thread.sleep(1000);
		
		//8-7  	ʶ��6B��  ,������,�ڶ�����Χ���޿�������� ,��Ӧʱ��ϳ�
		//8-7  	Identify 6B
		PassiveCommand.Identify6B(mADRcp);
		Thread.sleep(2000);
		
		//8-8  	��ȡ6B������,,6B���޷���,������,�ڶ�����Χ���޿�������� ,��Ӧʱ��ϳ�
		//8-8 	Read 6B
		iStart = 18;
		iLength = 4;
		PassiveCommand.Read6B(mADRcp, iStart, iLength);
		Thread.sleep(2000);
		
		//8-9	д��6B������ ,,6B���޷��� ,������,�ڶ�����Χ���޿�������� ,��Ӧʱ��ϳ�
		//8-9	Write 6B
		iStart = 18;
		iLength = 4;
		iData = new byte[]{0x01,0x02,0x03,0x04};
		PassiveCommand.Write6B(mADRcp, iStart, iLength,iData);
		Thread.sleep(2000);
		
	}
	
	//9-0 �Ͽ�����
	private static void disConnect(){
		mADRcp.disConnect();
	}
	
	public static void main(String[] args) {
		
		//3-0
		init();
		//6-0
		choiceCommType();
		//7-0
		connect();		
		try {
			//8-0
			runs();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		//9-0
		disConnect();
	}
}
