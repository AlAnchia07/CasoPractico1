IF NOT EXISTS (
    SELECT 1
    FROM dbo.UnidadesMedida
    WHERE Abreviatura = 'kg'
)
BEGIN
    INSERT INTO dbo.UnidadesMedida (Nombre, Abreviatura)
    VALUES ('Kilogramo', 'kg');
END;

IF NOT EXISTS (
    SELECT 1
    FROM dbo.UnidadesMedida
    WHERE Abreviatura = 'und'
)
BEGIN
    INSERT INTO dbo.UnidadesMedida (Nombre, Abreviatura)
    VALUES ('Unidad', 'und');
END;

SELECT * FROM dbo.UnidadesMedida;