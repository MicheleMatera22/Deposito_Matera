-- CREAZIONE TABELLE
CREATE TABLE Libri (
    id INT PRIMARY KEY,
    titolo VARCHAR(100),
    autore VARCHAR(100),
    genere VARCHAR(50),
    anno_pubblicazione INT,
    prezzo DECIMAL(6,2)
);

CREATE TABLE Vendite (
    id INT PRIMARY KEY,
    id_libro INT,
    data_vendita DATE,
    quantita INT,
    negozio VARCHAR(100)
);
-- INSERIMENTO VALORI
INSERT INTO Libri (id, titolo, autore, genere, anno_pubblicazione, prezzo) VALUES
(1, 'It', 'Stephen King', 'Horror', 1986, 19.90),
(2, 'Shining', 'Stephen King', 'Horror', 1977, 15.50),
(3, 'Misery', 'Stephen King', 'Horror', 1987, 14.00),
(4, 'Le notti di Salem', 'Stephen King', 'Horror', 1975, 12.00),
(5, 'Pet Sematary', 'Stephen King', 'Horror', 1983, 13.50),
(6, 'L''ombra dello scorpione', 'Stephen King', 'Horror', 1978, 22.00),
(7, 'La torre nera', 'Stephen King', 'Fantasy', 1982, 18.00),
(8, 'Il Signore degli Anelli', 'J.R.R. Tolkien', 'Fantasy', 1954, 25.00),
(9, 'Lo Hobbit', 'J.R.R. Tolkien', 'Fantasy', 1937, 14.00),
(10, 'Cronache del Ghiaccio e del Fuoco', 'George R.R. Martin', 'Fantasy', 1996, 24.50),
(11, 'Harry Potter e la Pietra Filosofale', 'J.K. Rowling', 'Fantasy', 1997, 16.00),
(12, 'La Compagnia Nera', 'Glen Cook', 'Fantasy', 1984, 17.50),
(13, 'Il nome del vento', 'Patrick Rothfuss', 'Fantasy', 2007, 20.00),
(14, 'Il nome della rosa', 'Umberto Eco', 'Giallo', 1980, 18.50),
(15, 'Dieci piccoli indiani', 'Agatha Christie', 'Giallo', 1939, 12.00),
(16, 'Il mastino dei Baskerville', 'Arthur Conan Doyle', 'Giallo', 1902, 10.00),
(17, 'Uomini che odiano le donne', 'Stieg Larsson', 'Giallo', 2005, 19.00),
(18, 'La ragazza del treno', 'Paula Hawkins', 'Giallo', 2015, 17.00),
(19, 'Il commissario Montalbano', 'Andrea Camilleri', 'Giallo', 1994, 13.00),
(20, '1984', 'George Orwell', 'Fantascienza', 1949, 14.00),
(21, 'Il mondo nuovo', 'Aldous Huxley', 'Fantascienza', 1932, 13.50),
(22, 'Dune', 'Frank Herbert', 'Fantascienza', 1965, 21.00),
(23, 'Sapiens', 'Yuval Noah Harari', 'Saggio', 2011, 20.00),
(24, 'Orgoglio e Pregiudizio', 'Jane Austen', 'Romanzo', 1813, 11.00),
(25, 'Guerra e Pace', 'Lev Tolstoj', 'Romanzo', 1869, 28.00),
(26, 'I Promessi Sposi', 'Alessandro Manzoni', 'Storico', 1827, 15.00),
(27, 'Il Gattopardo', 'G. Tomasi di Lampedusa', 'Storico', 1958, 16.00),
(28, 'L''insostenibile leggerezza dell''essere', 'Milan Kundera', 'Romanzo', 1984, 14.50),
(29, 'Cent''anni di solitudine', 'G. Garcia Marquez', 'Romanzo', 1967, 17.00),
(30, 'L''autobiografia di Martin Luther King', 'Martin Luther King Jr.', 'Saggio', 1998, 18.00);

INSERT INTO Vendite (id, id_libro, data_vendita, quantita, negozio) VALUES
(1, 1, '2023-01-15', 2, 'Libreria Centrale'),
(2, 8, '2023-01-16', 1, 'Bookcity Milano'),
(3, 15, '2023-01-17', 3, 'Cartoleria Roma'),
(4, 20, '2023-01-18', 1, 'Feltrinelli Roma'),
(5, 2, '2023-02-05', 1, 'Libreria Centrale'),
(6, 11, '2023-02-10', 5, 'Mondadori Napoli'),
(7, 23, '2023-02-12', 2, 'Online Store'),
(8, 30, '2023-02-15', 1, 'Bookcity Milano'),
(9, 14, '2023-03-01', 1, 'Cartoleria Roma'),
(10, 5, '2023-03-05', 2, 'Libreria Centrale'),
(11, 9, '2023-03-10', 1, 'Online Store'),
(12, 17, '2023-03-12', 1, 'Feltrinelli Roma'),
(13, 22, '2023-03-15', 3, 'Bookcity Milano'),
(14, 25, '2023-04-01', 1, 'Libreria del Corso'),
(15, 3, '2023-04-02', 1, 'Libreria Centrale'),
(16, 19, '2023-04-05', 2, 'Cartoleria Roma'),
(17, 29, '2023-04-10', 1, 'Mondadori Napoli'),
(18, 7, '2023-04-12', 1, 'Bookcity Milano'),
(19, 12, '2023-05-01', 1, 'Online Store'),
(20, 26, '2023-05-05', 1, 'Feltrinelli Roma'),
(21, 4, '2023-05-10', 1, 'Libreria Centrale'),
(22, 10, '2023-05-15', 2, 'Bookcity Milano'),
(23, 16, '2023-05-20', 1, 'Cartoleria Roma'),
(24, 21, '2023-06-01', 1, 'Online Store'),
(25, 28, '2023-06-05', 1, 'Mondadori Napoli'),
(26, 6, '2023-06-10', 1, 'Libreria Centrale'),
(27, 13, '2023-06-15', 2, 'Bookcity Milano'),
(28, 18, '2023-06-20', 1, 'Feltrinelli Roma'),
(29, 24, '2023-07-01', 3, 'Libreria Centrale'),
(30, 27, '2023-07-05', 1, 'Cartoleria Roma');

-- Es 1
SELECT * 
FROM libri
INNER JOIN vendite ON libri.id = vendite.id_libro
WHERE libri.autore LIKE '%king%';

-- Es 2
SELECT libri.titolo, libri.anno_pubblicazione, libri.prezzo, vendite.data_vendita
FROM libri
LEFT JOIN vendite ON libri.id = vendite.id_libro
WHERE libri.anno_pubblicazione BETWEEN 2000 AND 2010;

-- Es 3
SELECT libri.titolo, vendite.negozio, vendite.quantita, vendite.quantita*libri.prezzo AS prezzoTotale
FROM libri
INNER JOIN vendite ON libri.id = vendite.id_libro
WHERE negozio IN('libreria centrale','bookcity milano','cartoleria roma');

-- Es 4

INSERT INTO Vendite (id, id_libro, data_vendita, quantita, negozio) VALUES
(31, 13, '2021-06-15', 1, 'Bookcity Milano'),
(32, 99, '2022-01-20', 1, 'The Book-Shop');

SELECT libri.titolo, vendite.data_vendita, libri.prezzo, vendite.quantita
FROM Libri 
RIGHT JOIN Vendite ON libri.id = vendite.id_libro
WHERE
    vendite.data_vendita BETWEEN '2020-01-01' AND '2022-12-31'
    AND vendite.negozio LIKE '%book%';
    
-- Es 5

INSERT INTO Libri (id, titolo, autore, genere, anno_pubblicazione, prezzo) VALUES
(33, 'L''enigma della camera 622', 'Joël Dicker', 'Giallo', 2020, 20.00),
(34, 'Sesta di Corvi', 'Leigh Bardugo', 'Fantasy', 2016, 18.00);

INSERT INTO Vendite (id, id_libro, data_vendita, quantita, negozio) VALUES
(33, 33, '2023-08-01', 2, 'Online Store'),
(34, 34, '2023-07-15', 1, 'The BookStore Roma');

SELECT libri.titolo, libri.autore, libri.prezzo, vendite.data_vendita
FROM Libri 
JOIN Vendite ON libri.id = vendite.id_libro
WHERE libri.genere IN ('Fantasy', 'Horror', 'Giallo')
AND libri.anno_pubblicazione > 2015 AND vendite.negozio LIKE '%Store%'
ORDER BY vendite.data_vendita DESC;
  