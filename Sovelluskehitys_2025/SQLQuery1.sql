CREATE TABLE tuotteet (
	id INTEGER IDENTITY(1,1) PRIMARY KEY,
	nimi VARCHAR(100) NOT NULL,
	hinta DECIMAL(10, 2) NOT NULL,
	varastosaldo INT NOT NULL
);

INSERT INTO tuotteet (nimi, hinta, varastosaldo) VALUES
('Tuote A', 19.99, 50),
('Tuote B', 29.99, 30),
('Tuote C', 9.99, 100),
('Tuote D', 49.99, 20);

SELECT * FROM tuotteet;

DELETE FROM tuotteet WHERE id = 1002;