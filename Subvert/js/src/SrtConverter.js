function convertToMilliseconds(stringTime) {

    var millisecondsPerHour = 3600000;
    var millisecondsPerMinute = 60000;

    var hoursMinutesSecondsMilliseconds = stringTime.split(":");

    var hoursInMilliseconds = new Number(hoursMinutesSecondsMilliseconds[0]) * millisecondsPerHour;
    var minutesInMilliseconds = new Number(hoursMinutesSecondsMilliseconds[1]) * millisecondsPerMinute;

    var secondsAndMilliseconds = new Number(hoursMinutesSecondsMilliseconds[2].replace(',', ''));

    return (hoursInMilliseconds + minutesInMilliseconds + secondsAndMilliseconds);
};

function getTimesFromLine(line) {
	return null;
};