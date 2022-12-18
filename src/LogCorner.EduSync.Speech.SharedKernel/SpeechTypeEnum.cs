namespace LogCorner.EduSync.Speech.Command.SharedKernel
{
    public class SpeechTypeEnum
    {
        public int Value { get; }
        public string Name { get; }

        private SpeechTypeEnum()
        {
        }

        public SpeechTypeEnum(int value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}