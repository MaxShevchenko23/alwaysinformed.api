using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Interfaces
{
    public interface IArticleSandboxService:ICrud<ArticleSandboxGetDto,ArticleSandboxPostDto,ArticleSandboxUpdateDto>
    {

    }
}
