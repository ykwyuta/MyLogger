var outputFilePath = Environment.GetEnvironmentVariable("MY_LOGGER_OUTPUT_FILE_PATH") ?? ".";

var screenCaptureTask = Task.Run(async () => {
    var screenCapture = new ScreenCapture();
    while (true) {
        screenCapture.CaptureAllScreensToFile(outputFilePath: outputFilePath, scale: 200);
        await Task.Delay(1000);
    }
});
var audioRecorderTask = Task.Run(async () => {
    while (true) {
        var recorder = new LoopbackRecorder();
        recorder.StartRecording(outputFilePath: outputFilePath); // 録音を開始
        await Task.Delay(10000);
        recorder.StopRecording();
    }
});
await Task.WhenAll(screenCaptureTask, audioRecorderTask);