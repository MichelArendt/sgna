var userAgent = navigator.userAgent.toLowerCase();
var isElectron = false;
if (userAgent.indexOf(' electron/') > -1) {
	isElectron = true;
}

function GetIsElectron() {
	return isElectron;
}