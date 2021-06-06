-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           8.0.25 - MySQL Community Server - GPL
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Copiando estrutura do banco de dados para financas
CREATE DATABASE IF NOT EXISTS `financas`;
USE `financas`;

-- Copiando estrutura para tabela financas.CartaoCredito
CREATE TABLE IF NOT EXISTS `CartaoCredito` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `Bandeira` int NOT NULL,
  `DiaFechamentoFatura` int NOT NULL,
  `DiaVencimentoFatura` int NOT NULL,
  `Limite` decimal(18,2) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.CartaoCredito: ~2 rows (aproximadamente)
/*!40000 ALTER TABLE `CartaoCredito` DISABLE KEYS */;
INSERT INTO `CartaoCredito` (`Id`, `UserId`, `Bandeira`, `DiaFechamentoFatura`, `DiaVencimentoFatura`, `Limite`) VALUES
	(1, 1, 1, 10, 20, 10000.00),
	(2, 1, 0, 0, 0, 0.00);
/*!40000 ALTER TABLE `CartaoCredito` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.CartaoCreditoBandeira
CREATE TABLE IF NOT EXISTS `CartaoCreditoBandeira` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Bandeira` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.CartaoCreditoBandeira: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `CartaoCreditoBandeira` DISABLE KEYS */;
/*!40000 ALTER TABLE `CartaoCreditoBandeira` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.CartaoCreditoCompra
CREATE TABLE IF NOT EXISTS `CartaoCreditoCompra` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Desc` varchar(150) NOT NULL,
  `Valor` decimal(18,2) NOT NULL,
  `DataCompra` date NOT NULL,
  `QtdDeParcelas` int NOT NULL,
  `CartaoCreditoId` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.CartaoCreditoCompra: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `CartaoCreditoCompra` DISABLE KEYS */;
INSERT INTO `CartaoCreditoCompra` (`Id`, `Desc`, `Valor`, `DataCompra`, `QtdDeParcelas`, `CartaoCreditoId`) VALUES
	(1, 'Monitor', 400.00, '2021-06-02', 2, 1);
/*!40000 ALTER TABLE `CartaoCreditoCompra` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.CartaoCreditoParcela
CREATE TABLE IF NOT EXISTS `CartaoCreditoParcela` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CartaoCreditoCompraId` int NOT NULL,
  `VencimentoParcela` date NOT NULL,
  `ValorParcela` decimal(18,2) NOT NULL,
  `NumeroDaParcela` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.CartaoCreditoParcela: ~2 rows (aproximadamente)
/*!40000 ALTER TABLE `CartaoCreditoParcela` DISABLE KEYS */;
INSERT INTO `CartaoCreditoParcela` (`Id`, `CartaoCreditoCompraId`, `VencimentoParcela`, `ValorParcela`, `NumeroDaParcela`) VALUES
	(1, 1, '2021-06-20', 200.00, 1),
	(2, 1, '2021-07-20', 200.00, 2);
/*!40000 ALTER TABLE `CartaoCreditoParcela` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.CustoDiverso
CREATE TABLE IF NOT EXISTS `CustoDiverso` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `Desc` varchar(150) NOT NULL,
  `Valor` decimal(18,2) NOT NULL,
  `Pago` bit(1) NOT NULL,
  `Data` date NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.CustoDiverso: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `CustoDiverso` DISABLE KEYS */;
INSERT INTO `CustoDiverso` (`Id`, `UserId`, `Desc`, `Valor`, `Pago`, `Data`) VALUES
	(1, 1, 'Mesa PC', 120.00, b'0', '2021-06-03');
/*!40000 ALTER TABLE `CustoDiverso` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.CustoFixo
CREATE TABLE IF NOT EXISTS `CustoFixo` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CustoFixoDescricaoId` int NOT NULL,
  `Valor` decimal(18,6) NOT NULL,
  `Pago` bit(1) NOT NULL,
  `Data` date NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.CustoFixo: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `CustoFixo` DISABLE KEYS */;
/*!40000 ALTER TABLE `CustoFixo` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.CustoFixoDescricao
CREATE TABLE IF NOT EXISTS `CustoFixoDescricao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `Desc` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.CustoFixoDescricao: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `CustoFixoDescricao` DISABLE KEYS */;
/*!40000 ALTER TABLE `CustoFixoDescricao` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.Receita
CREATE TABLE IF NOT EXISTS `Receita` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `TipoDeReceita` int NOT NULL,
  `DataRecebimento` date NOT NULL,
  `Valor` decimal(18,6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.Receita: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `Receita` DISABLE KEYS */;
/*!40000 ALTER TABLE `Receita` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.TipoDeReceita
CREATE TABLE IF NOT EXISTS `TipoDeReceita` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.TipoDeReceita: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `TipoDeReceita` DISABLE KEYS */;
/*!40000 ALTER TABLE `TipoDeReceita` ENABLE KEYS */;

-- Copiando estrutura para tabela financas.Users
CREATE TABLE IF NOT EXISTS `Users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) NOT NULL,
  `Password` varchar(130) NOT NULL,
  `NomeCompleto` varchar(150) NOT NULL,
  `RefreshToken` varchar(500) DEFAULT NULL,
  `RefreshTokenExpiryTime` date DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- Copiando dados para a tabela financas.Users: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` (`Id`, `UserName`, `Password`, `NomeCompleto`, `RefreshToken`, `RefreshTokenExpiryTime`) VALUES
	(1, 'string', '47-32-87-F8-29-8D-BA-71-63-A8-97-90-89-58-F7-C0-EA-E7-33-E2-5D-2E-02-79-92-EA-2E-DC-9B-ED-2F-A8', 'string', 'yNc28S3i9xFg2v1uKPb0XsiqNN8CU9atE0lvI1qU/kA=', '2021-06-11');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
