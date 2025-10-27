CREATE TABLE Clienti (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    cognome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    eta INT,
    citta VARCHAR(100)
);

INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Mario', 'Rossi', 'mario.rossi@email.com', 30, 'Roma');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Luigi', 'Verdi', 'luigi.verdi@email.com', 25, 'Milano');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Anna', 'Bianchi', 'anna.bianchi@email.com', 45, 'Napoli');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Sofia', 'Russo', 'sofia.russo@email.com', 22, 'Torino');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Francesco', 'Ferrari', 'f.ferrari@email.com', 50, 'Firenze');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Giulia', 'Esposito', 'giulia.e@email.com', 28, 'Bologna');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Alessandro', 'Romano', 'ale.romano@email.com', NULL, 'Genova');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Chiara', 'Gallo', 'chiara.gallo@email.com', 19, 'Bari');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Matteo', 'Conti', 'm.conti@email.com', 60, 'Palermo');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Valentina', 'Costa', 'val_costa@email.com', 33, 'Catania');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Davide', 'Giordano', 'davideg@email.com', 42, NULL);
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Sara', 'Mancini', 'sara.mancini@email.com', 29, 'Verona');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Simone', 'Rizzo', 'simo.rizzo@email.com', 38, 'Messina');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Elena', 'Moretti', 'elena.moretti@email.com', 55, 'Padova');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Lorenzo', 'Bruno', 'l.bruno@email.com', 24, 'Trieste');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Federica', 'Barbieri', 'f.barbieri@email.com', 41, 'Brescia');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Giacomo', 'Marchetti', 'giacomo.m@email.com', 31, 'Parma');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Martina', 'Lombardi', 'marti.lombardi@email.com', NULL, NULL);
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Luca', 'Ricci', 'luca.ricci123@email.com', 48, 'Cagliari');
INSERT INTO Clienti (nome, cognome, email, eta, citta) VALUES ('Beatrice', 'Greco', 'bea.greco@email.com', 52, 'Perugia');

UPDATE Clienti
SET email = REPLACE(email, '@email.com', '@gmail.com')
WHERE id IN (3, 7, 10, 15);

-- 1
SELECT *
FROM Clienti
WHERE email LIKE '%gmail%';
-- 2
SELECT *
FROM Clienti
WHERE email LIKE '%@gmail.com';

-- 3
SELECT * 
FROM clienti
WHERE nome LIKE 'A%';

-- 4
SELECT *
FROM Clienti
WHERE nome LIKE '%A%';

-- 5
SELECT *
FROM Clienti
WHERE cognome REGEXP '^[A-Za-z]{5}$';

-- 6
SELECT * 
FROM Clienti
WHERE cognome LIKE '_____';

-- 7
SELECT *
FROM Clienti
WHERE eta BETWEEN 31 AND 40;

-- 8
SELECT *
FROM Clienti
WHERE eta BETWEEN 30 AND 40;

-- 9
SELECT * 
FROM Clienti
WHERE citta IN ('roma');

-- 10
SELECT * 
FROM Clienti
WHERE citta LIKE '%roma';

CREATE TABLE Ordini (
    ordine_id INT PRIMARY KEY AUTO_INCREMENT,
    cliente_id INT NOT NULL,
    data_ordine DATE NOT NULL,
    importo DECIMAL(10, 2) NOT NULL,
    prodotto_acquistato VARCHAR(100),
    FOREIGN KEY (cliente_id) REFERENCES Clienti(id)
);

INSERT INTO Ordini (cliente_id, data_ordine, importo, prodotto_acquistato) VALUES (1, '2025-01-15', 79.99, 'Cuffie Bluetooth');
INSERT INTO Ordini (cliente_id, data_ordine, importo, prodotto_acquistato) VALUES (3, '2025-01-17', 120.50, 'Tastiera Meccanica');
INSERT INTO Ordini (cliente_id, data_ordine, importo, prodotto_acquistato) VALUES (5, '2025-01-20', 45.00, 'Mouse Wireless');
INSERT INTO Ordini (cliente_id, data_ordine, importo, prodotto_acquistato) VALUES (1, '2025-02-01', 29.90, 'Cavo USB-C');
INSERT INTO Ordini (cliente_id, data_ordine, importo, prodotto_acquistato) VALUES (7, '2025-02-05', 499.00, 'Monitor 27"');
INSERT INTO Ordini (cliente_id, data_ordine, importo, prodotto_acquistato) VALUES (10, '2025-02-10', 19.50, 'Tappetino Mouse');
INSERT INTO Ordini (cliente_id, data_ordine, importo, prodotto_acquistato) VALUES (15, '2025-02-11', 85.00, 'Webcam HD');

-- inner join
SELECT 
    Clienti.nome, 
    Clienti.cognome, 
    Clienti.email, 
    Ordini.prodotto_acquistato, 
    Ordini.importo, 
    Ordini.data_ordine
FROM 
    Clienti
INNER JOIN 
    Ordini ON Clienti.id = Ordini.cliente_id;
    
-- right join
SELECT *
FROM Clienti
RIGHT JOIN 
    Ordini ON Clienti.id = Ordini.cliente_id;
    
-- left join

SELECT *
FROM Ordini
LEFT JOIN 
    Clienti ON Clienti.id = Ordini.cliente_id;
    
    
-- cross join

select * from clienti
cross join ordini
