using Asys.Domain.Common;

namespace Asys.Domain
{
    public class VideoActor : BaseDomainModel
    {
        public int VideoId { get; set; }
        
        public int ActorId { get; set; }
    }
}