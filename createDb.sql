-- Crear la base de datos
CREATE DATABASE InterRapidisimoDb;
GO

-- Usar la base de datos recién creada
USE InterRapidisimoDb;
GO

-- Crear tabla de estudiantes
CREATE TABLE Students (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    SurName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL
);

-- Crear tabla de programas de crédito
CREATE TABLE CreditPrograms (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL
);

-- Crear tabla de materias
CREATE TABLE Subjects (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL
);

-- Crear tabla de profesores
CREATE TABLE Professors (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL
);

-- Crear tabla intermedia StudentCreditProgram (relación muchos a muchos)
CREATE TABLE StudentCreditPrograms (
    StudentId UNIQUEIDENTIFIER NOT NULL,
    CreditProgramId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (StudentId, CreditProgramId),
    FOREIGN KEY (StudentId) REFERENCES Students(Id) ON DELETE CASCADE,
    FOREIGN KEY (CreditProgramId) REFERENCES CreditPrograms(Id) ON DELETE CASCADE
);

-- Crear tabla intermedia ProfessorSubject (relación muchos a muchos)
CREATE TABLE ProfessorSubjects (
    ProfessorId UNIQUEIDENTIFIER NOT NULL,
    SubjectId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (ProfessorId, SubjectId),
    FOREIGN KEY (ProfessorId) REFERENCES Professors(Id) ON DELETE CASCADE,
    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id) ON DELETE CASCADE
);

-- Crear tabla intermedia StudentSubject (relación muchos a muchos con Profesor)
CREATE TABLE StudentSubjects (
    StudentId UNIQUEIDENTIFIER NOT NULL,
    SubjectId UNIQUEIDENTIFIER NOT NULL,
    ProfessorId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (StudentId, SubjectId),
    FOREIGN KEY (StudentId) REFERENCES Students(Id) ON DELETE CASCADE,
    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id) ON DELETE RESTRICT,
    FOREIGN KEY (ProfessorId) REFERENCES Professors(Id) ON DELETE RESTRICT
);
