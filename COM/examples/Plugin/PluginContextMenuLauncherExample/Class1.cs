//------------------------------------------------------------------
// Navisworks Sample code
//------------------------------------------------------------------
//
// (C) Copyright 2011 by Autodesk Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//------------------------------------------------------------------
// Navisworks API
// A very basic plugin that adds an option to the Navisworks contex 
// menu to launch Notepad
//------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PluginContextMenuLauncherExample
{
   // Identify plugin with a suitable Guid and ProgId
   [ComVisible(true)]
   [Guid("EE58D7C7-E918-489B-8C73-56809A548455")]
   [ProgId("ADSK.LaunchNotepadPlugin")]

   // Plugin should be derived from "both" InwPlugin and InwExportPlugin
   public class LaunchNotepadPlugin : NavisworksIntegratedAPI19.InwPlugin,
                                   NavisworksIntegratedAPI19.InwExportPlugin
   {
      private NavisworksIntegratedAPI19.InwPlugin_Site m_plugin_site;
      private Process m_process;


      //
      // InwPlugin methods
      //

      public virtual void AdviseSite(NavisworksIntegratedAPI19.InwPlugin_Site plugin_site)
      {
         m_plugin_site = plugin_site;
      }
      public virtual bool iActivate()
      {
         return true;
      }
      public virtual void iAppInitialising()
      { ;}
      public virtual void iAppTerminating()
      { ;}
      public virtual bool iDeactivate()
      {
         return true;
      }
      public virtual void iDoCustomOption()
      { ;}
      public virtual String iGetDisplayName()
      {
         return "LaunchNotepadPlugin";
      }
      public virtual int iGetNumParameters()
      {
         return 0;
      }
      public virtual String iGetParameter(int ndx, ref object pData)
      {
         return null;
      }
      public virtual bool iSetParameter(int ndx, object newVal)
      {
         return false;
      }
      public virtual object iXtension(object vIn)
      {
         return null;
      }
      public virtual string ObjectName
      {
         get { return "LaunchNotepadObject"; }
      }
      //
      // InwExportPlugin methods
      //
      public virtual void iBeginContext(NavisworksIntegratedAPI19.InwOpState p_state)
      {
      }
      public virtual bool iCanExport(NavisworksIntegratedAPI19.InwOpState p_state, int reason)
      {
         return true;
      }
      public virtual bool iDialog(NavisworksIntegratedAPI19.InwOpState p_state, int reason)
      {
         return true;
      }
      public virtual void iEndContext(NavisworksIntegratedAPI19.InwOpState p_state)
      {
      }
      public virtual NavisworksIntegratedAPI19.nwEExportStatus iExport(NavisworksIntegratedAPI19.InwOpState p_state, int reason, string filename, NavisworksIntegratedAPI19.InwOpProgress Progress)
      {
         // Open Notepad
         m_process = new Process();
         m_process.StartInfo.FileName = @"Notepad.exe";
         m_process.EnableRaisingEvents = true;

         m_process.Start();
         m_process.WaitForInputIdle(1000);

         return NavisworksIntegratedAPI19.nwEExportStatus.eExport_OK;
      }
      public virtual string iGetFormatName()
      {
         return "Com exportplugin1 FN";
      }
      public virtual string iGetFormatSuffix()
      {
         return "Com exportplugin1 FS";
      }
      public virtual string iGetUserString()
      {
         // This is the identifying string that will appear
         // in the context menu.
         return "ADSK Example - Launch Notepad";
      }
      public virtual bool iHelpExport()
      {
         return false;
      }
      public virtual void InitialisePlugin(ref int capbits, ref int ver)
      {
         // Identify the plugin as a plugin context menu item.
         NavisworksIntegratedAPI19.nwECaps cb = NavisworksIntegratedAPI19.nwECaps.eCap_EXPORT_EXPLICIT | NavisworksIntegratedAPI19.nwECaps.eCap_EXPORT_CONTEXT_ITEM;
         capbits = (int)cb;
      }
   }
}
