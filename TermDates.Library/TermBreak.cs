namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System;

    public class TermBreak
    {
        public TermBreak(DateTime start, TimeSpan length) 
        {
            this.Start = start;
            this.Length = length;   
        }
                
        public DateTime Start { get; private set;}
        public TimeSpan Length { get; private set;}

        public virtual bool WithinBreak(DateTime day)
        {
            return ( day >= Start && day <= Start + Length);
        }
    }

    public class NullBreak : TermBreak
    {
        public NullBreak() : base(DateTime.Now, new TimeSpan(0,0,0))
        {}

        public override bool WithinBreak(DateTime day)
        {
            return false;
        }
    }
}