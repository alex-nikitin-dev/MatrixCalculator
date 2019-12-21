namespace MatrixCalculator
{
    //Model: [LeftSubstring][LeftObject][OPERATION ^*+-][RightObject][RightSubstring]
    public class Objects
    {
        public string LeftObject { get; set; }
        public string LeftSubstring { get; set; }
        public string RightObject { get; set; }
        public string RightSubstring { get; set; }

        public Objects()
        {
            LeftObject = "";
            LeftSubstring = "";
            RightObject = "";
            RightSubstring = "";
        }
    }
}

