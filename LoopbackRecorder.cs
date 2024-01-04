using NAudio.Wave;
using System;

class LoopbackRecorder
{
    private WasapiLoopbackCapture capture;
    private WaveFileWriter writer;

    public void StartRecording(string outputFilePath = ".")
    {
        string fileName = $"mylogger_{DateTime.Now.ToString("yyyyMMddHHmmss")}.wav";
        string filePath = String.Format("{0}/{1}", outputFilePath, fileName);
        capture = new WasapiLoopbackCapture();
        writer = new WaveFileWriter(filePath, capture.WaveFormat);

        capture.DataAvailable += (sender, e) =>
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
        };

        capture.RecordingStopped += (sender, e) =>
        {
            if (writer == null) return;
            writer.Dispose();
            writer = null;
            capture.Dispose();
        };

        capture.StartRecording();
    }

    public void StopRecording()
    {
        capture.StopRecording();
    }
}

// // 使用例
// var recorder = new LoopbackRecorder();
// recorder.StartRecording("loopback.wav"); // 録音を開始
// // 適当なタイミングで録音を停止
// // recorder.StopRecording();
