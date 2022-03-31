namespace Domain
{
    public class Professional
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Service { get; set; }
        public ulong CNPJ { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        private bool Active = true;


        public void SetUpdatedAt(DateTime date)
        {   
            UpdatedAt = date;
        }

        public bool GetActive()
        {
            var lastUpdate = DateTime.Now.Subtract(UpdatedAt).TotalDays;
            if (lastUpdate > 90)
            {
                Active = false;
            } 
            return Active;
        }
    }
}