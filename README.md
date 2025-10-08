## Migraciones

Para poder crear migraciones debemos instalar `dotnet-ef` de manera global

```bash
dotnet tool install --global dotnet-ef
```

También se puede actualizar, para esto utilizaremos el comando

```bash
dotnet tool update --global dotnet-ef
```

[Entity Framework Core tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

Una vez instalado dotnet-ef debemos instalar [EF Desing](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/10.0.0-rc.1.25451.107)

### Crear una migración

Cuando creamos una migración con el comando `dotnet ef migrations add {nombre_migración}`
nos genera diversos archivos. Nos genera la migración con los cambios a la base de datos
y nos genera un **snapshot** de la base de datos en ese momento.

Todas las migraciones cuentan con 2 métodos, **Up** y **Down**.

**Up**: Aplica todos los cambios con lo que se actualizó la base de datos.
**Down**: Revierte los cambios realizados.

### Ejecutan migraciones

> [WARNING]
> Si queremos utilizar migrations en producción debemos hacerlo una vez que se haya terminado
> el esquema, o sea mientras no hayan datos.

Una vez creadas nuestra migraciones las podremos ejecutar con el comando
`dotnet ef database update`
