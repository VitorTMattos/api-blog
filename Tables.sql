Create Table Posts (
id int primary key Identity(1,1),
titulo varchar(150),
conteudo varchar(max),
status_post int, 
data_criacao datetime Default(GETDATE())
);

Create Table Comentarios (
id int primary key Identity(1,1),
conteudo varchar(max),
status_comentario int, 
data_criacao datetime Default(GETDATE()),
id_post int, 
Constraint  FK_Comentarios_Posts Foreign key (id_post) References Posts(id) ON DELETE CASCADE
);

