namespace GerEventos.Permissions;
public static class GerEventosPermissions
{
    public const string GroupName = "GerEventos";

    public static class Evento
    {
        public const string Default = GroupName + ".Evento";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class TipoEvento
    {
        public const string Default = GroupName + ".TipoEvento";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Desativar = Default + ".Desativar";
        public const string Ativar = Default + ".Ativar";
    }

    public static class BalcaoVendas
    {
        public const string Default = GroupName + ".BalcaoVendas";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Desativar = Default + ".Desativar";
        public const string Ativar = Default + ".Ativar";
    }

    // Add your own permission names. Example:
    // public const string MyPermission1 = GroupName + ".MyPermission1";
}
