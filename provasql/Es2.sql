-- Creazione Tabella Clienti
CREATE TABLE Clienti (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    citta VARCHAR(100) NOT NULL
);

-- Creazione Tabella Ordini
CREATE TABLE Ordini (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_cliente INT NOT NULL,
    data_ordine DATE NOT NULL,
    importo DECIMAL(7, 2) NOT NULL,
    FOREIGN KEY (id_cliente) REFERENCES Clienti(id)
);

INSERT INTO Clienti (nome, citta) VALUES
('Mario Rossi', 'Roma'),
('Luigi Verdi', 'Milano'),
('Anna Bianchi', 'Napoli'),
('Sofia Russo', 'Torino'),
('Francesco Ferrari', 'Firenze'),
('Giulia Esposito', 'Bologna'),
('Alessandro Romano', 'Genova'),
('Chiara Gallo', 'Bari'),
('Matteo Conti', 'Palermo'),
('Valentina Costa', 'Catania'),
('Davide Giordano', 'Venezia'),
('Sara Mancini', 'Verona'),
('Simone Rizzo', 'Messina'),
('Elena Moretti', 'Padova'),
('Lorenzo Bruno', 'Trieste'),
('Federica Barbieri', 'Brescia'),
('Giacomo Marchetti', 'Parma'),
('Martina Lombardi', 'Prato'),
('Luca Ricci', 'Cagliari'),
('Beatrice Greco', 'Perugia');

INSERT INTO Ordini (id_cliente, data_ordine, importo) VALUES
(1, '2025-01-10', 150.50),
(3, '2025-01-12', 75.00),
(5, '2025-01-15', 300.20),
(2, '2025-01-18', 45.80),
(1, '2025-01-20', 80.00),
(7, '2025-01-22', 120.00),
(10, '2025-01-25', 55.40),
(4, '2025-02-01', 200.00),
(6, '2025-02-03', 10.50),
(15, '2025-02-05', 99.99),
(12, '2025-02-10', 450.00),
(9, '2025-02-11', 25.00),
(8, '2025-02-14', 130.10),
(18, '2025-02-15', 700.00),
(20, '2025-02-20', 30.00),
(19, '2025-02-22', 44.80),
(11, '2025-03-01', 95.00),
(13, '2025-03-02', 12.00),
(3, '2025-03-05', 300.00),
(1, '2025-03-07', 50.00);

-- Esercizio 1
SELECT 
	Clienti.nome, 
    Ordini.data_ordine, 
    Ordini.Importo
FROM Clienti
INNER JOIN Ordini ON Clienti.id = Ordini.id_cliente;

-- Esercizio 2
SELECT
  Clienti.nome,
  Ordini.data_ordine,
  Ordini.importo
FROM
  Clienti
  LEFT JOIN Ordini ON Clienti.id = Ordini.id_cliente;
  
  -- Esercizio 3
  SELECT
  Clienti.nome,
  Ordini.data_ordine,
  Ordini.importo
FROM
  Clienti
  RIGHT JOIN Ordini ON Clienti.id = Ordini.id_cliente;
  
  -- Esercizio 4
SELECT
  Clienti.nome,
  COUNT(Ordini.id) AS NumeroOrdini,
  SUM(Ordini.importo) AS ImportoTotaleSpeso
FROM
  Clienti
  INNER JOIN Ordini ON Clienti.id = Ordini.id_cliente
GROUP BY
  Clienti.nome, Clienti.id;
  
  -- Esercizio 5
SELECT
  Clienti.nome,
  Clienti.citta
FROM
  Clienti
  LEFT JOIN Ordini ON Clienti.id = Ordini.id_cliente
WHERE
  Ordini.id_cliente IS NULL;
  
  -- Esercizio 6
  SELECT
	Ordini.id,
    Ordini.data_ordine,
    Ordini.importo
  FROM
	Clienti
    RIGHT JOIN Ordini ON Clienti.id = Ordini.id_cliente
  WHERE
    Ordini.id_cliente IS NULL;
    
    