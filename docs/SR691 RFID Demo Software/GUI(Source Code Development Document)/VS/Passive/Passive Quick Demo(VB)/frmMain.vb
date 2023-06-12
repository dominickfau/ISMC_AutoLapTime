Imports ADSDK
Imports ADSDK.Device
Imports ADSDK.Device.Reader.Passive

Public Class frmMain

    'Dim adx As ActiveXPCOM          '串口版
    'Dim adx As ActiveXPNET          '网络版
    'Dim adx As ActiveXPUSB          'USB版
    Dim adx As ADActiveX            '通用版

    Public WithEvents adxEvent As ADActiveX '定义事件


    Dim FCount As Integer

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adx = New ADActiveX
        adxEvent = adx

        CheckForIllegalCrossThreadCalls = False

    End Sub

    '主动工作模式下,可使用这个功能,被动模式下可取消此命令
    Private Sub adxEvent_RxRspParsed(sender As Object, e As ProtocolEventArg) Handles adxEvent.RxRspParsed

        Select Case (e.Protocol.Code)
            Case PassiveRcp.RCP_CMD_EPC_IDEN
                If e.Protocol.Length > 0 And e.Protocol.Type = PassiveRcp.RCP_MSG_GET Then
                    ShowResultState("EPC No.: " + ByteArrayToHexString(e.Protocol.Payload, 1, e.Protocol.Length - 1))
                End If

        End Select

    End Sub

    Private Sub adxEvent_StatusConnected(sender As Object, e As ConnectEventArg) Handles adxEvent.StatusConnected
        Select Case (e.Status)
            Case CommState.CONNECT_OK
                ShowResultState("Interface is connected,but not sure dev is connected,need send command to determined it!")
            Case CommState.CONNECT_DEV_OK
                ShowResultState("dev is connected!")
            Case CommState.DISCONNECT_OK
                ShowResultState("Interface is disconnect!")
            Case CommState.CONNECT_FAIL
                ShowResultState("connect failed!")
            Case CommState.DISCONNECT_FAIL
                ShowResultState("disconnect failed!")
            Case CommState.DISCONNECT_EXCEPT
                ShowResultState("Remote abnormal disconnection!")
        End Select
        Debug.WriteLine(e.Status.ToString())


    End Sub

    Public Function ByteArrayToHexString(ByVal DecNumber() As Byte) As String
        Dim indata As String = ""
        For i As Int16 = 0 To (DecNumber.Length - 1)

            If DecNumber(i) <= 15 Then
                indata = indata & "0" & Hex(DecNumber(i))
            Else : indata = indata & Hex(DecNumber(i))
            End If
        Next
        ByteArrayToHexString = indata
    End Function

    Public Function ByteArrayToHexString(ByVal DecNumber() As Byte, index As Integer, count As Integer) As String
        Dim indata As String = ""
        For i As Int16 = 0 To count - 1

            If DecNumber(i + index) <= 15 Then
                indata = indata & "0" & Hex(DecNumber(i + index))
            Else : indata = indata & Hex(DecNumber(i + index))
            End If
        Next
        ByteArrayToHexString = indata
    End Function

    Public Function HexStringToByteArray(ByVal strmsg As String) As Byte()
        Dim indata(strmsg.Length / 2) As Byte
        Dim i, n As Integer
        For i = 1 To Len(strmsg) Step 2
            ReDim Preserve indata(n)
            indata(n) = Val("&H" & Mid(strmsg, i, 2))
            n = n + 1
        Next
        HexStringToByteArray = indata
    End Function

    Private Sub ShowResultState(str As String)
        If txtRes.Lines.Length > 5000 Then
            txtRes.Clear()

        End If
        txtRes.Text += DateTime.Now.ToString() + " " + str + Environment.NewLine
        txtRes.SelectionStart = txtRes.TextLength
        txtRes.ScrollToCaret()

    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        btnConnect.Enabled = False
        If Not adx.IsConnected Then
            adx.Connect(txtIP.Text, Convert.ToInt32(txtport.Text))
        Else
            adx.DisConnect()
        End If

        If adx.IsConnected Then
            btnConnect.Text = "DisConnect"
        Else
            btnConnect.Text = "Connect"
        End If
        btnConnect.Enabled = True
    End Sub

    Private Sub btnInformation_Click(sender As Object, e As EventArgs) Handles btnInformation.Click

        FCount = adx.Information()
        If FCount = 0 Then
            ShowResultState(adx.RecvString)
        End If
        ShowResultState(adx.GetReturnCode(FCount))
    End Sub

    Private Sub btnGetConfig_Click(sender As Object, e As EventArgs) Handles btnGetConfig.Click

        Dim bytdata(28) As Byte

        FCount = adx.pGetConfig(bytdata)
        If FCount = 0 Then
            ShowResultState(ByteArrayToHexString(bytdata))
        End If
        ShowResultState(adx.GetReturnCode(FCount))
    End Sub

    Private Sub btnInitActive_Click(sender As Object, e As EventArgs) Handles btnInitActive.Click

        Dim strmsg As String = "1E016E545D666F7882020A0001001E0A0F0110010102000200000020"
        FCount = adx.pSetConfig(HexStringToByteArray(strmsg))
        ShowResultState(adx.GetReturnCode(FCount))

    End Sub

    Private Sub btnInitPassive_Click(sender As Object, e As EventArgs) Handles btnInitPassive.Click
        'Reference communication protocol
        Dim strmsg As String = "1E016E545D666F7882030A0001001E0A0F0110010102000200000020"
        FCount = adx.pSetConfig(HexStringToByteArray(strmsg))
        ShowResultState(adx.GetReturnCode(FCount))

    End Sub


    Private Sub btnIdentify_Click(sender As Object, e As EventArgs) Handles btnIdentify.Click

        Dim bytdata(28) As Byte

        FCount = adx.pIdentify6C(bytdata)
        If FCount = 0 Then
            utxtCard.Text = (ByteArrayToHexString(bytdata)).Substring(2)
        Else
            utxtCard.Text = adx.GetReturnCode(FCount)
        End If
        ShowResultState(adx.GetReturnCode(FCount))
    End Sub


    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click

        Dim bytdata(28) As Byte

        Dim mem As Byte = Convert.ToByte(cmbReadBlock.SelectedIndex)
        Dim start As Byte = Convert.ToByte(ltxtReadAddress.Text)
        Dim Len As Byte = Convert.ToByte(ltxtReadLength.Text)

        FCount = adx.pRead6C(mem, start, Len, bytdata)

        If FCount = 0 Then
            utxtReadData.Text = (ByteArrayToHexString(bytdata)).Substring(2)
        Else
            utxtReadData.Text = adx.GetReturnCode(FCount)
        End If
        ShowResultState(adx.GetReturnCode(FCount))
    End Sub

    Private Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click

        Dim mem As Byte = Convert.ToByte(cmbReadBlock.SelectedIndex)
        Dim start As Byte = Convert.ToByte(ltxtReadAddress.Text)
        Dim Len As Byte = Convert.ToByte(ltxtReadLength.Text)

        Dim sndData() As Byte = HexStringToByteArray(utxtWriteData.Text.Replace("-", ""))

        FCount = adx.pWrite6C(mem, start, Len, sndData)

        ShowResultState(adx.GetReturnCode(FCount))
    End Sub

End Class
