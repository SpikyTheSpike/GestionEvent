CREATE TABLE [dbo].[InscriptionEvent]
(
	[Inscription_Id] INT NOT NULL IDENTITY,
	[NombreParticipant] INT NOT NULL,
	[Member_Id] INT NOT NULL,
	[Event_Id] INT  NOT NULL,
	[Remarque] NVARCHAR(500),

	CONSTRAINT FK_Event_Member
		FOREIGN KEY([Member_Id])
		REFERENCES [Member]([Member_Id]),
	CONSTRAINT FK_Event_Event
		FOREIGN KEY([Event_Id])
		REFERENCES [Event]([Event_Id])  ON DELETE CASCADE ,
	CONSTRAINT PK_Inscription PRIMARY KEY ([Inscription_Id])

)
