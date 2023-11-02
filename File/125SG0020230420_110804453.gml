<?xml version="1.0" encoding="UTF-8"?>
<Dataset xmlns="http://www.iho.int/S125/gml/cs0/1.0" xmlns:S100="http://www.iho.int/s100gml/5.0" xmlns:S201="http://www.iho.int/S201/gml/cs0/1.0" xmlns:gml="http://www.opengis.net/gml/3.2" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" gml:id="ds1" xsi:schemaLocation="http://www.iho.int/S125/gml/cs0/1.0 S-125_GML.xsd">
	<gml:boundedBy>
		<gml:Envelope srsDimension="2" srsName="EPSG:4326">
			<gml:lowerCorner>103.8486472 1.25235</gml:lowerCorner>
			<gml:upperCorner>103.8486472 1.25235</gml:upperCorner>
		</gml:Envelope>
	</gml:boundedBy>
	<S100:DatasetIdentificationInformation>
		<S100:encodingSpecification>S-100 Part 10b</S100:encodingSpecification>
		<S100:encodingSpecificationEdition>1.0</S100:encodingSpecificationEdition>
		<S100:productIdentifier>INT.IHO.S-125.1.1.0</S100:productIdentifier>
		<S100:productEdition>1.0</S100:productEdition>
		<S100:applicationProfile>1</S100:applicationProfile>
		<S100:datasetFileIdentifier>test.000</S100:datasetFileIdentifier>
		<S100:datasetTitle>This File is created by KRISO Viewer.</S100:datasetTitle>
		<S100:datasetReferenceDate>2023-04-20</S100:datasetReferenceDate>
		<S100:datasetLanguage>eng</S100:datasetLanguage>
		<S100:datasetTopicCategory>utilitiesCommunication</S100:datasetTopicCategory>
		<S100:datasetPurpose>base</S100:datasetPurpose>
		<S100:updateNumber>0</S100:updateNumber>
	</S100:DatasetIdentificationInformation>
	<members>
		<AtoNStatusInformation gml:id="INFO_ID_0004">
			<changeDetails>
			</changeDetails>
			<changeTypes>Discrepancy</changeTypes>
		</AtoNStatusInformation>
		<AtoNStatusInformation gml:id="INFO_ID_0003">
			<changeTypes>Proposed changes</changeTypes>
		</AtoNStatusInformation>
		<AtoNStatusInformation gml:id="INFO_ID_0002">
			<changeTypes>Advance notice of changes</changeTypes>
		</AtoNStatusInformation>
		<AtoNStatusInformation gml:id="INFO_ID_0001">
			<changeTypes>Temporary changes</changeTypes>
		</AtoNStatusInformation>
		<BeaconSpecialPurposeGeneral gml:id="FEATURE_ID_0001">
			<categoryOfSpecialPurposeMark>yellow</categoryOfSpecialPurposeMark>
			<beaconShape>Beacon Tower</beaconShape>
			<atonStatus xlink:arcrole="http://www.iho.int/S125/roles/atonStatus" xlink:href="#INFO_ID_0003" xlink:title="atonStatusInformation"/>
			<geometry>
				<S100:pointProperty>
					<S100:Point gml:id="POINT_1">
						<gml:pos>103.8486472 1.25235</gml:pos>
					</S100:Point>
				</S100:pointProperty>
			</geometry>
		</BeaconSpecialPurposeGeneral>
	</members>
</Dataset>
