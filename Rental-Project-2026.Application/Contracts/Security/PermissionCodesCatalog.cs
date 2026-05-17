using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.Contracts.Security
{
    public static class PermissionCodesCatalog
    {
        //Users
        public const string SHOW_USERS = "showUsers";
        public const string CREATE_USERS = "createUsers";
        public const string EDIT_USERS = "editUsers";
        public const string DELETE_USERS = "deleteUsers";

        //Vehicles
        public const string SHOW_VEHICLES = "showVehicles";
        public const string CREATE_VEHICLES = "createVehicles";
        public const string EDIT_VEHICLES = "editVehicles";
        public const string DELETE_VEHICLES = "deleteVehicles";

        //Branches
        public const string SHOW_BRANCHES = "showBranches";
        public const string CREATE_BRANCHES = "createBranches";
        public const string EDIT_BRANCHES = "editBranches";
        public const string DELETE_BRANCHES = "deleteBranches";

        public readonly record struct PermissionSeed(string Code, string Description, string Module);

        public static IReadOnlyList<PermissionSeed> All { get; } = new List<PermissionSeed>
            {
                new PermissionSeed(SHOW_USERS, "Ver Usuarios", "Usuarios"),
                new PermissionSeed(CREATE_USERS, "Crear Usuarios", "Usuarios"),
                new PermissionSeed(EDIT_USERS, "Editar Usuarios", "Usuarios"),
                new PermissionSeed(DELETE_USERS, "Eliminar Usuarios", "Usuarios"),

                new PermissionSeed(SHOW_VEHICLES, "Ver Vehículos", "Vehículos"),
                new PermissionSeed(CREATE_VEHICLES, "Crear Vehículos", "Vehículos"),
                new PermissionSeed(EDIT_VEHICLES, "Editar Vehículos", "Vehículos"),
                new PermissionSeed(DELETE_VEHICLES, "Eliminar Vehículos", "Vehículos"),

                new PermissionSeed(SHOW_BRANCHES, "Ver Sucursales", "Sucursales"),
                new PermissionSeed(CREATE_BRANCHES, "Crear Sucursales", "Sucursales"),
                new PermissionSeed(EDIT_BRANCHES, "Editar Sucursales", "Sucursales"),
                new PermissionSeed(DELETE_BRANCHES, "Eliminar Sucursales", "Sucursales"),

            };

            public static IReadOnlyList<string> AllCodes { get; } = All.Select(p => p.Code).ToList();
    };
    
}
