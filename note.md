```
chimes:
  - time: 0000
    preset: �՗t ��
    message: �ߑO0���ł�
  - time: 0100
    preset: �՗t ��
    message: �ߑO1���ł�
  - time: 0200
    preset: �՗t ��
    message: �ߑO2���ł�
  - time: 0300
    preset: �՗t ��
    message: �ߑO3���ł�
  - time: 0400
    preset: �՗t ��
    message: �ߑO4���ł�
  - time: 0500
    preset: �՗t ��
    message: �ߑO5���ł�
  - time: 0600
    preset: �՗t ��
    message: �ߑO6���ł�
  - time: 0700
    preset: �՗t ��
    message: �ߑO7���ł�
  - time: 0800
    preset: �՗t ��
    message: �ߑO8���ł�
  - time: 0900
    preset: �՗t ��
    message: �ߑO9���ł�
  - time: 1000
    preset: �՗t ��
    message: �ߑO10���ł�
  - time: 1100
    preset: �՗t ��
    message: �ߑO11���ł�
  - time: 1200
    preset: �՗t ��
    message: �ߌ�0���ł�
  - time: 1300
    preset: �՗t ��
    message: �ߌ�1���ł�
  - time: 1400
    preset: �՗t ��
    message: �ߌ�2���ł�
  - time: 1500
    preset: �՗t ��
    message: �ߌ�3���ł�
  - time: 1600
    preset: �՗t ��
    message: �ߌ�4���ł�
  - time: 1700
    preset: �՗t ��
    message: �ߌ�5���ł�
  - time: 1800
    preset: �՗t ��
    message: �ߌ�6���ł�
  - time: 1900
    preset: �՗t ��
    message: �ߌ�7���ł�
  - time: 2000
    preset: �՗t ��
    message: �ߌ�8���ł�
  - time: 2100
    preset: �՗t ��
    message: �ߌ�9���ł�
  - time: 2200
    preset: �՗t ��
    message: �ߌ�10���ł�
  - time: 2300
    preset: �՗t ��
    message: �ߌ�11���ł�

```

```
        // �T���v���f�[�^�̏�������
        private static void WriteSample()
        {
            // �������� �ʓ|�Ȃ̂łЂ�����add line
            StreamWriter writer = new StreamWriter(configPath, true, encoding);

            writer.WriteLine("chimes:");

            //// string�^�̔z���24�p��
            //// �������_�T������c#��foreach�̓C���f�b�N�X���̂�������Ɩʓ|...
            //for (int i = 0; i < 24; i++)
            //{
            //    // �������2����0����
            //    string zeroParsedTime = i.ToString("D2");
            //    writer.WriteLine($"  - time: {zeroParsedTime}00");

            //    // �����������爩�A��Ȃ爨
            //    string kotonohaPreset = (i % 2 == 0) ? "�՗t ��" : "�՗t ��";
            //    writer.WriteLine($"    preset: {kotonohaPreset}");
            //    writer.WriteLine($"    message: {i}���ł�");
            //}

            for (int i = 0; i < 12; i++)
            {
                string zeroParsedTime = i.ToString("D2");
                writer.WriteLine($"  - time: {zeroParsedTime}00");

                string kotonohaPreset = (i % 2 == 0) ? "�՗t ��" : "�՗t ��";
                writer.WriteLine($"    preset: {kotonohaPreset}");
                writer.WriteLine($"    message: �ߑO{i}���ł�");
            }
            for (int i = 12; i < 24; i++)
            {
                string zeroParsedTime = i.ToString("D2");
                writer.WriteLine($"  - time: {zeroParsedTime}00");

                string kotonohaPreset = (i % 2 == 0) ? "�՗t ��" : "�՗t ��";
                writer.WriteLine($"    preset: {kotonohaPreset}");
                writer.WriteLine($"    message: �ߌ�{i-12}���ł�");
            }

            // �J������߂�
            writer.Close();
        }
```

  �ォ��
 - 0557: �p�X
 �݂����ɂ��Ă������Ɩ�悤�ɂ�����

 �t�@�C���p�X�͎����ɂ��Đ��������{�C�X�ŏ㏑�����Ă��΂悳����

�T���v��yml�̓R�[�h���琶������K�v�͂Ȃ��̂ŏ����Ă悳����

AI�{�C�X���N������
yml�ǂݍ���
yml�̃��b�Z�[�W�ƃv���Z�b�g�ɉ����ă{�C�X����
�{�C�X��exe���K�w�ɕۑ�(/voice)

windows���玞�����擾
�A�v������P�����ƂɎ���
�荏�ɂȂ�����wav��炷


�O���������������
�v���Ă����Z�ʂ������Ȃ��Ėʓ|��������AIVOICE

1. �c�[�� �v���W�F�N�g�ݒ� �t�@�C���ۑ��_�C�A���O�őI������
2. �c�[�� ���ݒ� ���b�Z�[�W �Ȍ�


�N��
config�ǂݍ���
config�ɉ�����wav�ǂݍ��݁��f�B���N�g����K���ɓǂނق����悳����

�P���Ԋu�̌��ݎ�������ɔc�����Ă����H
������s����

AIvoice�Ń{�C�X�f�[�^��ۑ�����@�\�ƃt�@�C������{�C�X�f�[�^��ǂݍ��ދ@�\�͊��S�ɕ�����
�Ԃɓ���͍̂\���ʂ�ɕ��񂾃f�B���N�g���ƃt�@�C��

�t�@�C����ǂݍ��ށ�������dict�ɓ��ꍞ�ށ�

�f�B���N�g���͋N�����Ɉ�x�擾���邯��wav�͖��񃉃��_�����肵����
�ĂȂ�ƃt�@�C���p�X�����擾���āA�荏�ɂȂ����痐��������


            //[
            //  {"0100":["1���ł�.wav","1�����.wav"},
            //  {"0200":["2���ł�.wav","2�����.wav"}
            //]

�����[�h�{�^���H���b�t�@�C����ǂݍ��݂Ȃ����Ă����������H

- �v���Z�b�g���Ȃ��ꍇ�̃G���[