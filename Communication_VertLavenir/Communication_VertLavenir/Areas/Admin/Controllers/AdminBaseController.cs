using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Communication_VertLavenir.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Staff))]
    public abstract class AdminBaseController : Controller
    {
        protected readonly AppDbContext Db;

        protected AdminBaseController(AppDbContext db)
        {
            Db = db;
        }
    }
}
