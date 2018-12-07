using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BTI7252.Models;

namespace BTI7252.DataAccess
{
	public interface ICouchRepository
	{
		Task<ValidationResult> Save(ThingModel model);
	}
}