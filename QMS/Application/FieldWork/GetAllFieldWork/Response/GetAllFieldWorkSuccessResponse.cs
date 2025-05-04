
namespace Application.FieldWork.GetAllFieldWork.Response
{
    public class GetAllFieldWorkSuccessResponse : GetAllFieldWorkResponse
    {
        public IEnumerable<FieldWorkDTO> FieldworksDTO {  get; set; } = new List<FieldWorkDTO>();
    }
}
