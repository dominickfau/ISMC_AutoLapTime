var rAdax;
var rFCount;
var rRet;

function disconnect() {
    if (rAdax != null) {
        rAdax.DisConnect();
        rAdax = null;
    }
    changeConnectState(false);
}

function connect() {
    var host = "192.168.2.116";
    var rateorport = "49152";

    disconnect();

    if (rAdax == null) {
        rAdax = document.getElementById("ADActiveXPNet");
    }

    host = document.getElementById("host").value;
    rateorport = parseInt(document.getElementById("port").value, 10);

    rAdax.jsConnect(host, rateorport);

    changeConnectState(rAdax.IsConnected);

    rRet = "State:" + rAdax.IsConnected;

    insertResult(rRet);
}

function information() {

    rFCount = rAdax.jspInformation();

    rRet = "获取信息" + getrFCount(rFCount) + rAdax.RecvString;
    changestate(rFCount == 0);
    insertResult(rRet);
}

function init() {
    rFCount = rAdax.jspSetConfig("1E 01 6E 54 5D 66 6F 78 82 03 0A 00 03 00 1E 0A 0F 01 10 01 01 02 00 02 00 00 00 20");

    rRet = "初始化参数" + getrFCount(rFCount) + "-Code:" + rFCount;
    insertResult(rRet);
}

function identify6c() {
    rFCount = rAdax.jspIdentify6C();

    if (rFCount == 0) {
        var length = rAdax.RecvString.length;
        var svalue = rAdax.RecvString.substr(2, length - 2);   //前1字节为天线号可忽略
        document.getElementById("rIdentifyData").value = svalue;
    } else {
        document.getElementById("rIdentifyData").value = "";
    }
    rRet = "识别" + getrFCount(rFCount) + "-Code:" + rFCount;
    insertResult(rRet);
}

function identify6cmult() {
    rFCount = rAdax.jspIdentify6CMult();

    if (rFCount == 0) {
        document.getElementById("rResult").value = rAdax.RecvString;
    } else {
        document.getElementById("rResult").value = "";
    }
    rRet = "多卡识别" + getrFCount(rFCount) + "-Code:" + rFCount;
    insertResult(rRet);
}

function read6c() {
    var mem = document.getElementById("rmb").selectedIndex;
    var state = parseInt(document.getElementById("rsa").value, 10);
    var len = parseInt(document.getElementById("rdl").value, 10);
    rFCount = rAdax.jspRead6C(mem, state, len);

    if (rFCount == 0) {
        var length = rAdax.RecvString.length;
        var svalue = rAdax.RecvString.substr(2, length - 2);   //前1字节为天线号可忽略
        document.getElementById("rReadData").value = svalue;
    } else {
        document.getElementById("rReadData").value = "";
    }
    rRet = "读取" + getrFCount(rFCount) + "-Code:" + rFCount;
    insertResult(rRet);
}

function write6c() {
    var mem = document.getElementById("rmb").selectedIndex;
    var state = parseInt(document.getElementById("rsa").value, 10);
    var len = parseInt(document.getElementById("rdl").value, 10);
    var data = document.getElementById("rWriteData").value;
    rFCount = rAdax.jspWrite6C(mem, state, len, data);

    rRet = "写入" + getrFCount(rFCount) + "-Code:" + rFCount;
    insertResult(rRet);
}

function getrFCount(fcount) {

    if (fcount == 0 || fcount == 10) {
        return "成功";
    } else {
        return "失败";
    }
}

function insertResult(ret) {
    var e = document.getElementById("rResult");
    e.value += "[" + new Date().toLocaleString() + "] " + ret + "\n\r";
    e.scrollTop = e.scrollHeight;
}

function changeConnectState(fcount) {
    if (fcount) {
        document.getElementById("rConnect").disabled = "disabled";
        document.getElementById("rDisConnect").disabled = "";
        document.getElementById("rInformation").disabled = "";
        document.getElementById("rInit").disabled = "disabled";
        document.getElementById("rIdentify").disabled = "disabled";
        document.getElementById("rRead").disabled = "disabled";
        document.getElementById("rWrite").disabled = "disabled";
    }
    else {
        document.getElementById("rConnect").disabled = "";
        document.getElementById("rDisConnect").disabled = "disabled";
        document.getElementById("rInformation").disabled = "disabled";
        document.getElementById("rInit").disabled = "disabled";
        document.getElementById("rIdentify").disabled = "disabled";
        document.getElementById("rRead").disabled = "disabled";
        document.getElementById("rWrite").disabled = "disabled";
    }
}

function changestate(fcount) {
    if (fcount) {
        document.getElementById("rInit").disabled = "";
        document.getElementById("rIdentify").disabled = "";
        document.getElementById("rRead").disabled = "";
        document.getElementById("rWrite").disabled = "";
    }
    else {
        document.getElementById("rInit").disabled = "disabled";
        document.getElementById("rIdentify").disabled = "disabled";
        document.getElementById("rRead").disabled = "disabled";
        document.getElementById("rWrite").disabled = "disabled";
    }
}