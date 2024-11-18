using DomainProperty.Models.DataModels;

namespace DomainProperty.Repositories
{
    public class CsvDataRepository : IDataRepository
    {
        private  readonly string _csvFilePath;


        public CsvDataRepository(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
            
          //  LoadProperties(csvFilePath);
        }



        public async Task<List<Property>> GetPropertiesAsync()
        {
            var properties = new List<Property>();

            var allRows = await File.ReadAllLinesAsync(_csvFilePath);
            var dataRows = allRows.Skip(1);


            foreach (var row in dataRows) // Skip header
            {
                var columns = row.Split(',');
                properties.Add(new Property
                {
                    Id = int.Parse(columns[0]),
                    Suburb = columns[1],
                    Value = int.Parse(columns[2]),
                    Date = columns[3],
                    NumberOfBedrooms = int.Parse(columns[4]),
                    Type = columns[5]
                });
            }

            return properties;
        }



        // load at start of application approach
        //private static readonly List<Property> Properties = new List<Property>();
        //private static void LoadProperties(string filePath)
        //{

        //    var allRows =  File.ReadAllLines(filePath);
        //    var dataRows = allRows.Skip(1);

        //    foreach (var row in dataRows) // Skip header
        //    {
        //        var columns = row.Split(',');
        //        Properties.Add(new Property
        //        {
        //            Id = int.Parse(columns[0]),
        //            Suburb = columns[1],
        //            Value = int.Parse(columns[2]),
        //            Date = columns[3],
        //            NumberOfBedrooms = int.Parse(columns[4]),
        //            Type = columns[5]
        //        });
        //    }
        //}

        //public async Task<List<Property>> GetPropertiesAsync()
        //{
        //    return Properties;
        //}

    }
}
