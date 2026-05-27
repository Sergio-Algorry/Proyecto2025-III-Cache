using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto2025_III.BD.Datos;
using Proyecto2025_III.Shared.DTO;
using Proyecto2025_III.Shared.ENUM;

namespace Proyecto2025_III.Repositorio.Seguridad;

public class ServicioSeguridad : IServicioSeguridad
{
    private readonly AppDbContext context;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IHttpContextAccessor contextAccesor;
    private readonly IAuthorizationService authorizationService;

    public ServicioSeguridad(AppDbContext context,
                             UserManager<ApplicationUser> userManager,
                             IHttpContextAccessor contextAccesor,
                             IAuthorizationService authorizationService)
    {
        this.context = context;
        this.userManager = userManager;
        this.contextAccesor = contextAccesor;
        this.authorizationService = authorizationService;
    }

    public async Task<ResultadoOperacionSeguridad> HacerAdmin(UsuarioDTO entidad)
    {
        try
        {
            //var usuarioLogueado = contextAccesor.HttpContext.User;
            //var resultado = await authorizationService.AuthorizeAsync(usuarioLogueado, "admin");

            //if (!resultado.Succeeded)
            //{
            //    return ResultadoOperacionSeguridad.SinPermiso;
            //}
            var usuario = await userManager.FindByEmailAsync(entidad.Email);

            if (usuario == null)
            {
                return ResultadoOperacionSeguridad.NoEncontrado;
            }

            await userManager.AddToRoleAsync(usuario, "admin");
            await userManager.UpdateSecurityStampAsync(usuario);

            return ResultadoOperacionSeguridad.Exitoso;
        }
        catch (Exception e)
        {
            return ResultadoOperacionSeguridad.Fallido;
        }
    }

    public async Task<ResultadoOperacionSeguridad> RemoverAdmin(string email)
    {
        //var usuarioLogueado = contextAccesor.HttpContext.User;
        //var resultado = await authorizationService.AuthorizeAsync(usuarioLogueado, "admin");

        //if (!resultado.Succeeded)
        //{
        //    return ResultadoOperacionSeguridad.SinPermiso;
        //}
        var usuario = await userManager.FindByEmailAsync(email);

        if (usuario == null)
        {
            return ResultadoOperacionSeguridad.NoEncontrado;
        }

        await userManager.RemoveFromRoleAsync(usuario, "admin");
        await userManager.UpdateSecurityStampAsync(usuario);
        return ResultadoOperacionSeguridad.Exitoso;
    }

    public async Task<List<UsuarioDTO>> ObtenerUsuarios()
    {
        return await context.Users.OrderBy(x => x.UserName)
            .Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Email = u.Email!
            })
            .AsNoTracking()
            .ToListAsync();
    }
}
