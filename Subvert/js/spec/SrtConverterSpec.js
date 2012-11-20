describe("Tests the conversion of formatted time into a numeric milliseconds value", function() {
    it("tests that the millisecond portion of the time gets converted into a number", function() {
       var stringTime = "00:00:00,345";
       var milliseconds = 345;
       
       expect(convertToMilliseconds(stringTime)).toEqual(milliseconds);
    });
 
    it("tests that seconds and milliseconds get converted into a number", function() {
       var stringTime = "00:00:15,012";
       var milliseconds = 15012;
       
       expect(convertToMilliseconds(stringTime)).toEqual(milliseconds); 
    });
    
    it("tests that minutes, seconds, and milliseconds get converted into a number", function() {
       var stringTime = "00:15:05,100";
       var milliseconds = 905100;
       
       expect(convertToMilliseconds(stringTime)).toEqual(milliseconds); 
    });
    
    it("tests that hours, minutes, seconds, and milliseconds get converted into a number", function() {
       var stringTime = "10:15:05,100";
       var milliseconds = 36905100;
       
       expect(convertToMilliseconds(stringTime)).toEqual(milliseconds); 
    });
});

describe("Tests the reading of a line with subtitle times on it and extracts the times", function() {
  it("has a starting time and an ending time", function() {
    var lineOfFile = "00:10:06,123 --> 00:10:25,543";
    var startTimeMilliseconds = 606123;
    var endTimeMilliseconds = 625543;

    var timeLine = {startTime: startTimeMilliseconds, endTime: endTimeMilliseconds};

    var result = getTimesFromLine(lineOfFile);

    expect(result[startTime].toEqual(startTimeMilliseconds));
    expect(result[endTime].toEqual(endTimeMilliseconds));   
  });
});