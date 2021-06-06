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

-- Copiando estrutura para tabela financas.CartaoCreditoBandeira
CREATE TABLE IF NOT EXISTS `CartaoCreditoBandeira` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Bandeira` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

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

-- Copiando estrutura para tabela financas.CartaoCreditoParcela
CREATE TABLE IF NOT EXISTS `CartaoCreditoParcela` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CartaoCreditoCompraId` int NOT NULL,
  `VencimentoParcela` date NOT NULL,
  `ValorParcela` decimal(18,2) NOT NULL,
  `NumeroDaParcela` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

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

-- Copiando estrutura para tabela financas.CustoFixo
CREATE TABLE IF NOT EXISTS `CustoFixo` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CustoFixoDescricaoId` int NOT NULL,
  `Valor` decimal(18,6) NOT NULL,
  `Pago` bit(1) NOT NULL,
  `Data` date NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Copiando estrutura para tabela financas.CustoFixoDescricao
CREATE TABLE IF NOT EXISTS `CustoFixoDescricao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `Desc` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Copiando estrutura para tabela financas.Receita
CREATE TABLE IF NOT EXISTS `Receita` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `TipoDeReceita` int NOT NULL,
  `DataRecebimento` date NOT NULL,
  `Valor` decimal(18,6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Copiando estrutura para tabela financas.TipoDeReceita
CREATE TABLE IF NOT EXISTS `TipoDeReceita` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

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

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
