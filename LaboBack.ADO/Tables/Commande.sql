CREATE TABLE [dbo].[Commande]
(
	[Id] INT IDENTITY,
	[UtilisateurId] INT NOT NULL,
	[DateCommande] DATETIME NOT NULL,
	[StatutCommande] NVARCHAR(50) NOT NULL,

	CONSTRAINT [PK_Commande] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Commande_Utilisateur] FOREIGN KEY ([UtilisateurId]) REFERENCES [dbo].[Utilisateur] ([Id])
)
