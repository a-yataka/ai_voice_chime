# 設定ファイルに必要な情報
- 時間帯と合わせたメッセージ
- 時間帯指定はcronベース（まったく同じは面倒なのでそこまでしなくてもよい）
- 

```
chimes:
  - time: 0000
    preset: a
    message: b
  - time: 0100
    preset: a
    message: b
  - time: 0200
    preset: a
    message: b
  - time: 0300
    preset: a
    message: b
  - time: 0400
    preset: a
    message: b
  - time: 0500
    preset: a
    message: b
  - time: 0600
    preset: a
    message: b
  - time: 0700
    preset: a
    message: b
  - time: 0800
    preset: a
    message: b
  - time: 0900
    preset: a
    message: b
  - time: 1000
    preset: a
    message: b
  - time: 1100
    preset: a
    message: b
  - time: 1200
    preset: a
    message: b
  - time: 1300
    preset: a
    message: b
  - time: 1400
    preset: a
    message: b
  - time: 1500
    preset: a
    message: b
  - time: 1600
    preset: a
    message: b
  - time: 1700
    preset: a
    message: b
  - time: 1800
    preset: a
    message: b
  - time: 1900
    preset: a
    message: b
  - time: 2000
    preset: a
    message: b
  - time: 2100
    preset: a
    message: b
  - time: 2200
    preset: a
    message: b
  - time: 2300
    preset: a
    message: b
```

  後から
 - 0557: パス
 みたいにしてもちゃんと鳴るようにしたい

 ファイルパスは時刻にして生成したボイスで上書きしてけばよさそう