/*******************************************************************************
  * Copyright (C) 2015 AgGateway and ADAPT Contributors
  * Copyright (C) 2015 Deere and Company
  * All rights reserved. This program and the accompanying materials
  * are made available under the terms of the Eclipse Public License v1.0
  * which accompanies this distribution, and is available at
  * http://www.eclipse.org/legal/epl-v10.html <http://www.eclipse.org/legal/epl-v10.html> 
  *
  * Contributors:
  *    Tim Shearouse - initial API and implementation
  *******************************************************************************/
using AgGateway.ADAPT.Representation.UnitSystem.ExtensionMethods;
using System;
using System.IO;
using System.Reflection;

namespace AgGateway.ADAPT.Representation.UnitSystem
{
    public static class UnitSystemManager
    {
        private static string unitSystemDataLocation = null;
        public static string UnitSystemDataLocation {
            get {
                if (unitSystemDataLocation == null) {
                    var assemblyLocation = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                    var repSystemXml = Path.Combine(assemblyLocation, "Resources", "UnitSystem.xml");
                    return repSystemXml;
                } else {
                    return unitSystemDataLocation;
                }
            }
            set { unitSystemDataLocation = value; }
        }

        public static ApplicationDataModel.Common.UnitOfMeasure GetUnitOfMeasure(string code)
        {
            return InternalUnitSystemManager.Instance.UnitOfMeasures[code].ToModelUom();
        }

        public static ApplicationDataModel.Common.UnitOfMeasure FromUNRec20Code(string code)
        {
            var domainId = InternalUnitSystemManager.Instance.UnRec20CodeToDomainId[code];
            return GetUnitOfMeasure(domainId);
        }

        public static string GetUNRec20Code(ApplicationDataModel.Common.UnitOfMeasure uom)
        {
            return InternalUnitSystemManager.Instance.DomainIdToUnRec20Code[uom.Code];
        }
    }
}
