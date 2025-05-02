CREATE TABLE [dbo].[Utilisateur]
(
	[Id] INT IDENTITY,
	[Nom] NVARCHAR(100) NOT NULL,
	[Prenom] NVARCHAR(100) NOT NULL,
	[Email] NVARCHAR(255) NOT NULL,
	[MdpHash] NVARCHAR(500) NOT NULL,
	[Role] NVARCHAR(50)
	


	CONSTRAINT [PK_Utilisateur] PRIMARY KEY ([Id]),
	CONSTRAINT [UK_Utilisateur] UNIQUE ([Email])
)


