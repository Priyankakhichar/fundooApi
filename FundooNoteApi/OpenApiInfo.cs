using Swashbuckle.AspNetCore.Swagger;

namespace FundooNoteApi
{
    internal class OpenApiInfo : Info
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}