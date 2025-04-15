using DbWebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbWebApplication;

public class UserController(
    ILogger<UserController> logger,
    StudentService studentService,
    QrCodeService qrCodeService)
    : Controller
{

    
}