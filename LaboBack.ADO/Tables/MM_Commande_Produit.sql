CREATE TABLE [dbo].[MM_Commande_Produit]
(
	[CommandeId] INT NOT NULL,
	[ProduitId] INT NOT NULL, 
	[QuantiteCommandee] INT,
	CONSTRAINT [FK_MM_Commande] FOREIGN KEY([CommandeId]) REFERENCES[dbo].[Commande] ([Id]),
	CONSTRAINT [FK_MM_Produit] FOREIGN KEY([ProduitId]) REFERENCES[dbo].[Produit] ([Id]),
	CONSTRAINT [PK_MM_Commande_Produit] PRIMARY KEY ([CommandeId], [ProduitId])
)
