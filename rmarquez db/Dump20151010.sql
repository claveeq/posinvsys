CREATE DATABASE  IF NOT EXISTS `rmarquez` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `rmarquez`;
-- MySQL dump 10.13  Distrib 5.6.24, for Win64 (x86_64)
--
-- Host: localhost    Database: rmarquez
-- ------------------------------------------------------
-- Server version	5.6.26-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `account`
--

DROP TABLE IF EXISTS `account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account` (
  `acc_id` int(11) NOT NULL AUTO_INCREMENT,
  `acc_name` varchar(45) NOT NULL,
  `acc_surname` varchar(45) NOT NULL,
  `acc_age` int(11) DEFAULT NULL,
  `acc_pass` varchar(45) NOT NULL,
  `acc_bday` date DEFAULT '0000-00-00',
  `acc_admin` tinyint(1) unsigned zerofill DEFAULT '0',
  `acc_address` varchar(100) DEFAULT NULL,
  `acc_gender` tinyint(1) unsigned zerofill DEFAULT '0',
  PRIMARY KEY (`acc_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES (1,'Clave','Quimbo',NULL,'1234','1996-09-08',0,'1662 A&V Subd. Sulucan, Bocaue, bulacan',0),(2,'Lem','De Guzman',NULL,'1234','1995-05-17',0,'1037 v.Mauricio st. Turo,Bocaue,Bulacan',0),(3,'Justin','Mendoza',NULL,'1234','1996-02-17',0,'161 Bunlo Bocaue Bulacan',0),(4,'Neeve','Quimbo',NULL,'hello','2015-09-23',0,'655756',0),(5,'Gemma','Gibaga',NULL,'1234','2014-08-21',0,'Bocaue, Bulacan',1);
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `brand`
--

DROP TABLE IF EXISTS `brand`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `brand` (
  `brand_id` int(11) NOT NULL AUTO_INCREMENT,
  `brand_name` varchar(45) NOT NULL,
  PRIMARY KEY (`brand_id`),
  UNIQUE KEY `brand_id_UNIQUE` (`brand_id`),
  UNIQUE KEY `brand_name_UNIQUE` (`brand_name`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `brand`
--

LOCK TABLES `brand` WRITE;
/*!40000 ALTER TABLE `brand` DISABLE KEYS */;
INSERT INTO `brand` VALUES (7,'Bonus'),(1,'cla er'),(2,'dsf'),(3,'g'),(4,'ghjg'),(6,'johnson'),(5,'Microsoft');
/*!40000 ALTER TABLE `brand` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventory`
--

DROP TABLE IF EXISTS `inventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `inventory` (
  `inv_barcode` varchar(45) NOT NULL,
  `inv_stock` varchar(45) DEFAULT NULL,
  `inv_ropoint` varchar(45) DEFAULT NULL,
  `inv_roamount` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`inv_barcode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory`
--

LOCK TABLES `inventory` WRITE;
/*!40000 ALTER TABLE `inventory` DISABLE KEYS */;
INSERT INTO `inventory` VALUES ('48040693','50','6','Total Price'),('NUF-00001','20','5','Total Price');
/*!40000 ALTER TABLE `inventory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `location`
--

DROP TABLE IF EXISTS `location`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `location` (
  `loc_id` int(11) NOT NULL AUTO_INCREMENT,
  `loc_name` varchar(45) NOT NULL,
  PRIMARY KEY (`loc_id`),
  UNIQUE KEY `Loc_prod_UNIQUE` (`loc_name`),
  UNIQUE KEY `loc_id_UNIQUE` (`loc_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `location`
--

LOCK TABLES `location` WRITE;
/*!40000 ALTER TABLE `location` DISABLE KEYS */;
/*!40000 ALTER TABLE `location` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pricing`
--

DROP TABLE IF EXISTS `pricing`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pricing` (
  `price_barcode` varchar(40) NOT NULL,
  `price_supply` varchar(40) DEFAULT NULL,
  `price_markup` varchar(40) DEFAULT NULL,
  `price_price` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`price_barcode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pricing`
--

LOCK TABLES `pricing` WRITE;
/*!40000 ALTER TABLE `pricing` DISABLE KEYS */;
INSERT INTO `pricing` VALUES ('48040693','23','15','26.45'),('NUF-00001','800','14','912');
/*!40000 ALTER TABLE `pricing` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product` (
  `prod_id` int(11) NOT NULL AUTO_INCREMENT,
  `prod_barcode` varchar(45) NOT NULL,
  `prod_name` varchar(45) NOT NULL,
  `prod_description` varchar(100) DEFAULT NULL,
  `prod_type` varchar(45) DEFAULT NULL,
  `prod_brand` varchar(45) DEFAULT NULL,
  `prod_loc` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`prod_id`,`prod_barcode`),
  UNIQUE KEY `prod_id_UNIQUE` (`prod_id`),
  UNIQUE KEY `prod_barcode_UNIQUE` (`prod_barcode`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'48040693','Poweder','','324','34','234'),(2,'NUF-00001','Xbox','3453','45435','345','345');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receipt`
--

DROP TABLE IF EXISTS `receipt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `receipt` (
  `rec_id` int(11) NOT NULL AUTO_INCREMENT,
  `rec_date` date DEFAULT '0000-00-00',
  `rec_items` varchar(500) DEFAULT NULL,
  `rec_total` double DEFAULT NULL,
  `rec_cash` double DEFAULT NULL,
  `rec_change` double DEFAULT NULL,
  `rec_cog` double DEFAULT NULL,
  PRIMARY KEY (`rec_id`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receipt`
--

LOCK TABLES `receipt` WRITE;
/*!40000 ALTER TABLE `receipt` DISABLE KEYS */;
INSERT INTO `receipt` VALUES (1,'2015-10-08',NULL,494,543563,543069,NULL),(2,'2015-10-08',NULL,494,500,6,NULL),(3,'2015-10-08',NULL,494,5000,4506,NULL),(4,'2015-10-08',NULL,4945,500,6,NULL),(5,'2015-10-08',NULL,494,500,6,NULL),(6,'2015-10-08',NULL,494,500,6,NULL),(7,'2015-10-08',NULL,494,4575,4081,NULL),(8,'2015-10-08',NULL,494,1000,506,NULL),(9,'2015-10-08',NULL,494,500,6,NULL),(10,'2015-10-08',NULL,494,500,6,NULL),(11,'2015-10-08',NULL,494,500,6,NULL),(12,'2015-10-08',NULL,896,1000,104,NULL),(13,'2015-10-08',NULL,494,500,6,NULL),(14,'2015-10-08',NULL,494,500,6,NULL),(15,'2015-10-08',NULL,494,500,6,NULL),(16,'2015-10-08',NULL,494,5000,4506,NULL),(17,'2015-10-08',NULL,494,5050,4556,NULL),(18,'2015-10-08',NULL,494,500,6,NULL),(19,'2015-10-08',NULL,494,500,6,NULL),(20,'2015-10-08',NULL,494,5000,4506,NULL),(21,'2015-10-08',NULL,494,500,6,NULL),(22,'2015-10-08',NULL,494,500,6,NULL),(23,'2015-10-08',NULL,494,500,6,NULL),(24,'2015-10-08',NULL,494,500,6,NULL),(25,'2015-10-08',NULL,494,500,6,NULL),(26,'2015-10-08',NULL,494,500,6,NULL),(27,'2015-10-08',NULL,494,500,6,NULL),(28,'2015-10-08',NULL,896,500,-396,NULL),(29,'2015-10-08',NULL,494,500,6,NULL),(30,'2015-10-08',NULL,494,500,6,NULL),(31,'2015-10-08',NULL,494,500,6,NULL),(32,'2015-10-08',NULL,494,500,6,NULL),(33,'2015-10-08',NULL,21,50,29,NULL),(34,'2015-10-08',NULL,494,500,6,NULL),(35,'2015-10-08',NULL,494,4500,4006,NULL),(36,'2015-10-08',NULL,1976,500,-1476,NULL),(37,'2015-10-08',NULL,494,500,6,NULL),(38,'2015-10-08',NULL,26,26,0,NULL),(39,'2015-10-08',NULL,164,200,36,NULL),(40,'2015-10-08',NULL,1042530,1042530,0,NULL),(41,'2015-10-08',NULL,132,132,0,NULL),(42,'2015-10-08',NULL,991,991,0,NULL),(43,'2015-10-08',NULL,6384,991,-5393,NULL),(44,'2015-10-08',NULL,4560,4560,0,NULL),(45,'2015-10-08',NULL,912,912,0,NULL),(46,'2015-10-08',NULL,53,53,0,NULL),(47,'2015-10-08',NULL,912,912,0,NULL),(48,'2015-10-08',NULL,912,912,0,NULL),(49,'2015-10-08',NULL,912,912,0,NULL),(50,'2015-10-08',NULL,912,912,0,NULL),(51,'2015-10-09',NULL,912,912,0,NULL),(52,'2015-10-09',NULL,53,912,859,NULL),(53,'2015-10-09',NULL,2736,912,-1824,NULL),(54,'2015-10-09',NULL,912,912,0,NULL),(55,'2015-10-09',NULL,2736,2736,0,800),(56,'2015-10-09',NULL,2762.45,2736,-26.4499999999998,23),(57,'2015-10-09',NULL,2736,2736,0,800),(58,'2015-10-09',NULL,2736,2736,0,2400),(59,'2015-10-09',NULL,912,2736,1824,800),(60,'2015-10-10',NULL,26.45,26.45,0,23);
/*!40000 ALTER TABLE `receipt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales`
--

DROP TABLE IF EXISTS `sales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales` (
  `sales_id` int(11) NOT NULL,
  PRIMARY KEY (`sales_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales`
--

LOCK TABLES `sales` WRITE;
/*!40000 ALTER TABLE `sales` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type`
--

DROP TABLE IF EXISTS `type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `type` (
  `type_id` int(11) NOT NULL AUTO_INCREMENT,
  `type_name` varchar(45) NOT NULL,
  PRIMARY KEY (`type_id`),
  UNIQUE KEY `type_name_UNIQUE` (`type_name`),
  UNIQUE KEY `type_id_UNIQUE` (`type_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type`
--

LOCK TABLES `type` WRITE;
/*!40000 ALTER TABLE `type` DISABLE KEYS */;
INSERT INTO `type` VALUES (1,'Candy'),(6,'Candy chociko'),(8,'erw'),(11,'Game Console'),(3,'General'),(5,'Generalg'),(9,'powder'),(10,'School Supplies'),(7,'sdfsd');
/*!40000 ALTER TABLE `type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'rmarquez'
--

--
-- Dumping routines for database 'rmarquez'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-10-10 16:30:04
