```
chimes:
  - time: 0000
    preset: 琴葉 茜
    message: 午前0時です
  - time: 0100
    preset: 琴葉 葵
    message: 午前1時です
  - time: 0200
    preset: 琴葉 茜
    message: 午前2時です
  - time: 0300
    preset: 琴葉 葵
    message: 午前3時です
  - time: 0400
    preset: 琴葉 茜
    message: 午前4時です
  - time: 0500
    preset: 琴葉 葵
    message: 午前5時です
  - time: 0600
    preset: 琴葉 茜
    message: 午前6時です
  - time: 0700
    preset: 琴葉 葵
    message: 午前7時です
  - time: 0800
    preset: 琴葉 茜
    message: 午前8時です
  - time: 0900
    preset: 琴葉 葵
    message: 午前9時です
  - time: 1000
    preset: 琴葉 茜
    message: 午前10時です
  - time: 1100
    preset: 琴葉 葵
    message: 午前11時です
  - time: 1200
    preset: 琴葉 茜
    message: 午後0時です
  - time: 1300
    preset: 琴葉 葵
    message: 午後1時です
  - time: 1400
    preset: 琴葉 茜
    message: 午後2時です
  - time: 1500
    preset: 琴葉 葵
    message: 午後3時です
  - time: 1600
    preset: 琴葉 茜
    message: 午後4時です
  - time: 1700
    preset: 琴葉 葵
    message: 午後5時です
  - time: 1800
    preset: 琴葉 茜
    message: 午後6時です
  - time: 1900
    preset: 琴葉 葵
    message: 午後7時です
  - time: 2000
    preset: 琴葉 茜
    message: 午後8時です
  - time: 2100
    preset: 琴葉 葵
    message: 午後9時です
  - time: 2200
    preset: 琴葉 茜
    message: 午後10時です
  - time: 2300
    preset: 琴葉 葵
    message: 午後11時です

```

```
        // サンプルデータの書き込み
        private static void WriteSample()
        {
            // 書き込み 面倒なのでひたすらadd line
            StreamWriter writer = new StreamWriter(configPath, true, encoding);

            writer.WriteLine("chimes:");

            //// string型の配列を24個用意
            //// 書き方ダサいけどc#のforeachはインデックス取るのがちょっと面倒...
            //for (int i = 0; i < 24; i++)
            //{
            //    // 文字列を2桁の0埋め
            //    string zeroParsedTime = i.ToString("D2");
            //    writer.WriteLine($"  - time: {zeroParsedTime}00");

            //    // 偶数だったら茜、奇数なら葵
            //    string kotonohaPreset = (i % 2 == 0) ? "琴葉 茜" : "琴葉 葵";
            //    writer.WriteLine($"    preset: {kotonohaPreset}");
            //    writer.WriteLine($"    message: {i}時です");
            //}

            for (int i = 0; i < 12; i++)
            {
                string zeroParsedTime = i.ToString("D2");
                writer.WriteLine($"  - time: {zeroParsedTime}00");

                string kotonohaPreset = (i % 2 == 0) ? "琴葉 茜" : "琴葉 葵";
                writer.WriteLine($"    preset: {kotonohaPreset}");
                writer.WriteLine($"    message: 午前{i}時です");
            }
            for (int i = 12; i < 24; i++)
            {
                string zeroParsedTime = i.ToString("D2");
                writer.WriteLine($"  - time: {zeroParsedTime}00");

                string kotonohaPreset = (i % 2 == 0) ? "琴葉 茜" : "琴葉 葵";
                writer.WriteLine($"    preset: {kotonohaPreset}");
                writer.WriteLine($"    message: 午後{i-12}時です");
            }

            // 開けたら閉める
            writer.Close();
        }
```

  後から
 - 0557: パス
 みたいにしてもちゃんと鳴るようにしたい

 ファイルパスは時刻にして生成したボイスで上書きしてけばよさそう

サンプルymlはコードから生成する必要はないので消してよさそう

AIボイスを起動する
yml読み込み
ymlのメッセージとプリセットに沿ってボイス生成
ボイスをexe同階層に保存(/voice)

windowsから時刻を取得
アプリから１ｓごとに時刻
定刻になったらwavを鳴らす


前提条件が多すぎる
思ってたより融通が利かなくて面倒くさいぞAIVOICE

1. ツール プロジェクト設定 ファイル保存ダイアログで選択する
2. ツール 環境設定 メッセージ 簡潔


起動
config読み込み
configに沿ってwav読み込み→ディレクトリを適当に読むほうがよさそう

１分間隔の現在時刻を常に把握しておく？
毎回実行する

AIvoiceでボイスデータを保存する機能とファイルからボイスデータを読み込む機能は完全に分ける
間に入るのは構成通りに並んだディレクトリとファイル

ファイルを読み込む→時刻をdictに入れ込む→

ディレクトリは起動時に一度取得するけどwavは毎回ランダム決定したい
てなるとファイルパスだけ取得して、定刻になったら乱数動かす


            //[
            //  {"0100":["1時です.wav","1時やで.wav"},
            //  {"0200":["2時です.wav","2時やで.wav"}
            //]

リロードボタン？毎秒ファイルを読み込みなおしてもいいかも？

- プリセットがない場合のエラー