using Microsoft.Identity.Client.Extensibility;

namespace alwaysinformed_bll.Services
{
    public class PagingMetaInfo
    {
        public int totalItemCount;
        public int pageSize;
        public int currentPage;
        public int totalPagesAmount;

        public PagingMetaInfo(int totalItemCount, int pageSize, int currentPage)
        {
            this.totalItemCount = totalItemCount;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            totalPagesAmount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }
    }
}