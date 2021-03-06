﻿using System;
using AutoFixture;
using AutoFixture.Kernel;
using EMG.XML;
using NUnit.Framework;
using TestHelper;

namespace Tests.EMG20.Serialization {
    [TestFixture]
    public class EventPriceSerializationTests
    {
        private IFixture fixture;

        [SetUp]
        public void Initialize()
        {
            fixture = new Fixture();

            fixture.Customize<Import>(o => o.With(p => p.Version, 2.0m));

            fixture.Customize<InstituteNode>(o => o.With(p => p.Locations, new LocationNode[0]));

            fixture.Customize<EducationNode>(o => o
                                               .OmitAutoProperties()
                                               .With(p => p.UniqueIdentifier)
                                               .With(p => p.Name)
                                               .With(p => p.EducationTypeID)
                                               .With(p => p.Events)
                                               .With(p => p.ContentFields, new ContentField[0])
            );

            fixture.Customizations.Add(new TypeRelay(typeof(EventNode), typeof(DistanceEvent)));

            fixture.Customize<DistanceEvent>(o => o
                                                  .OmitAutoProperties()
                                                  .With(p => p.UniqueIdentifier)
                                                  .With(p => p.EventTypeID)
                                                  .With(p => p.Price)
            );

        }

        [Test]
        public void Minimal_event_price_node_can_be_serialized()
        {
            fixture.Customize<PriceNode>(o => o.OmitAutoProperties().With(p => p.Price));

            var import = fixture.Create<Import>();

            var serialized = import.ToXml();

            XmlSchemas.XmlImport.ValidateDocument(serialized);

            Console.Write(serialized);

        }

        [Test]
        public void Event_price_node_with_currency_can_be_serialized()
        {
            fixture.Customize<PriceNode>(o => o.OmitAutoProperties()
                                               .With(p => p.Price)
                                               .With(p => p.Currency)
            );

            var import = fixture.Create<Import>();

            var serialized = import.ToXml();

            XmlSchemas.XmlImport.ValidateDocument(serialized);

            Console.Write(serialized);
        }

        [Test]
        public void Event_price_node_with_vat_can_be_serialized()
        {
            fixture.Customize<PriceNode>(o => o.OmitAutoProperties()
                                               .With(p => p.Price)
                                               .With(p => p.VAT)
            );

            var import = fixture.Create<Import>();

            var serialized = import.ToXml();

            XmlSchemas.XmlImport.ValidateDocument(serialized);

            Console.Write(serialized);
        }

        [Test]
        public void Event_price_node_with_vatIncluded_can_be_serialized()
        {
            fixture.Customize<PriceNode>(o => o.OmitAutoProperties()
                                               .With(p => p.Price)
                                               .With(p => p.IsVatIncluded)
            );

            var import = fixture.Create<Import>();

            var serialized = import.ToXml();

            XmlSchemas.XmlImport.ValidateDocument(serialized);

            Console.Write(serialized);
        }

        [Test]
        public void Event_price_node_with_no_discount_can_be_serialized()
        {
            fixture.Customize<PriceNode>(o => o.OmitAutoProperties()
                                               .With(p => p.Price)
                                               .With(p => p.IsVatIncluded)
                                               .With(p => p.VAT)
                                               .With(p => p.Currency)
            );

            var import = fixture.Create<Import>();

            var serialized = import.ToXml();

            XmlSchemas.XmlImport.ValidateDocument(serialized);

            Console.Write(serialized);
        }
    }
}