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
// Navisworks API - AutoSelectionExample
// This application demonstrates how to keep track of objects selected 
// in the Navisworks interface and manipulate the selection. (e.g. making 
//  objects hidden or changing colour)
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;


namespace AutoSelectionExample
{
   public partial class Form1 : Form
   {
      private NavisworksIntegratedAPI19.InwOpState10 m_state;
      private NavisworksAutomationAPI19.Document m_doc;
      

      public Form1()
      {
         InitializeComponent();
         translateRadio.Select();
      }

      private void zoomButton_Click(object sender, EventArgs e)
      {
         if (m_state != null)
            m_state.ZoomInCurViewOnCurSel();
      }

      private void launchButton_Click(object sender, EventArgs e)
      {
         // Create a new Navisworks Document, this will launch Navisworks
         m_doc = new NavisworksAutomationAPI19.Document();
         // Read the state from the new Navisworks Document
         m_state = m_doc.State();
         // Tell Navisworks to be visible
         m_doc.Visible = true;

      }

      private void paletteButton_Click(object sender, EventArgs e)
      {
         ColorDialog colorDialog = new ColorDialog();
         if (colorDialog.ShowDialog() == DialogResult.OK)
         {
            // Change the colour on the currently selected object.
            NavisworksIntegratedAPI19.InwLVec3f colorVec = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            colorVec.SetValue(colorDialog.Color.R / 255.0, colorDialog.Color.G / 255.0, colorDialog.Color.B / 255.0);
            m_state.OverrideColor(m_state.CurrentSelection, colorVec);
         }
      }

      private void transparencyButton_Click(object sender, EventArgs e)
      {
         // When the user presses the transparency button the transparency
         // of the selected objects is set to 0.5
         m_state.OverrideTransparency(m_state.CurrentSelection, 0.5);
      }

      private void resetButton_Click(object sender, EventArgs e)
      {
            // Reset all overrides (transparency, position, scale, color etc.)
            m_state.OverrideResetAll();
            m_state.OverrideTransformResetAll();
      }
      private void transformByUp(ref NavisworksIntegratedAPI19.InwLVec3f vec, double xx, double yy, double zz)
      {
         NavisworksIntegratedAPI19.InwLVec3f upVector = m_state.CurrentView.ViewPoint.Camera.GetUpVector();
         vec.SetValue(xx, yy, zz);        
      }

      private void leftButton_Click(object sender, EventArgs e)
      {
         NavisworksIntegratedAPI19.InwLTransform3f3 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLTransform3f);
         if (translateRadio.Checked)
         {
            NavisworksIntegratedAPI19.InwLVec3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            transformByUp(ref transVal, -10.0, 0.0, 0.0);
            transVec.MakeTranslation(transVal);
         }
         else
         {
            NavisworksIntegratedAPI19.InwLVec3f scaleVal = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            transformByUp(ref scaleVal, 0.5, 1.0, 1.0);
            transVec.MakeScale(scaleVal);            
         }

         m_state.OverrideTransform(m_state.CurrentSelection, transVec);
      }

      private void upButton_Click(object sender, EventArgs e)
      {
         NavisworksIntegratedAPI19.InwLTransform3f2 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLTransform3f);
         if (translateRadio.Checked)
         {
            NavisworksIntegratedAPI19.InwLVec3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            transformByUp(ref transVal, 0.0, 10.0, 0.0);
            transVec.MakeTranslation(transVal);
         }
         else
         {
            NavisworksIntegratedAPI19.InwLVec3f scaleVal = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            transformByUp(ref scaleVal, 1.0, 2.0, 1.0);
            transVec.MakeScale(scaleVal);
         }
         m_state.OverrideTransform(m_state.CurrentSelection, transVec);
      }

      private void rightButton_Click(object sender, EventArgs e)
      {
         NavisworksIntegratedAPI19.InwLTransform3f2 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLTransform3f);
         if (translateRadio.Checked)
         {
            NavisworksIntegratedAPI19.InwLVec3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            transformByUp(ref transVal, 10.0, 0.0, 0.0);
            transVec.MakeTranslation(transVal);
         }
         else
         {
            NavisworksIntegratedAPI19.InwLVec3f scaleVal = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            transformByUp(ref scaleVal, 2.0, 1.0, 1.0);
            transVec.MakeScale(scaleVal);
         }
         m_state.OverrideTransform(m_state.CurrentSelection, transVec);
      }

      private void downButton_Click(object sender, EventArgs e)
      {
         NavisworksIntegratedAPI19.InwLTransform3f2 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLTransform3f);
         if (translateRadio.Checked)
         {
            NavisworksIntegratedAPI19.InwLVec3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            transformByUp(ref transVal, 0.0, -10.0, 0.0);
            transVec.MakeTranslation(transVal);
         }
         else
         {
            NavisworksIntegratedAPI19.InwLVec3f scaleVal = m_state.ObjectFactory(NavisworksIntegratedAPI19.nwEObjectType.eObjectType_nwLVec3f);
            transformByUp(ref scaleVal, 1.0, 0.5, 1.0);
            transVec.MakeScale(scaleVal);
         }
         m_state.OverrideTransform(m_state.CurrentSelection, transVec);
      }

      private void openButton_Click(object sender, EventArgs e)
      {
         OpenFileDialog fileDialog = new OpenFileDialog();
         fileDialog.Filter = "NWD files|*.nwd";
         fileDialog.Title = "Open Navisworks Document";
         DialogResult result = fileDialog.ShowDialog();
         if (result == DialogResult.OK)
         {
            string filename = fileDialog.FileName;
            m_doc.OpenFile(filename);
         }
      }

      private bool FindConcreteNode(ref NavisworksIntegratedAPI19.InwOaNode node)
      {
         // Find the concrete node from the specified node when selected.
         if (node == null) return false;
         if (node.IsInsert)
         {
            NavisworksIntegratedAPI19.InwOaGroup grp = node as NavisworksIntegratedAPI19.InwOaGroup;
            node = grp.Children().Last();
            return FindConcreteNode(ref node);
         }
         return true;
      }
      private void Update_Click(object sender, EventArgs e)
      {
         selectionList.Items.Clear();
         NavisworksIntegratedAPI19.InwOpState10 l_state = m_state as NavisworksIntegratedAPI19.InwOpState10;
         // Find the currently selected nodes.
         NavisworksIntegratedAPI19.InwSelectionPathsColl selPaths = m_state.CurrentSelection.Paths();
         foreach (NavisworksIntegratedAPI19.InwOaPath pPath in selPaths)
         {
            NavisworksIntegratedAPI19.InwPathNodesColl pNodes = pPath.Nodes();
            NavisworksIntegratedAPI19.InwOaNode selectedNode = pNodes.Last();
            if (FindConcreteNode(ref selectedNode))
            {
               if (selectedNode.UserName != "")
                  selectionList.Items.Add(selectedNode.UserName);
               else
               {
                  selectionList.Items.Add(selectedNode.ClassUserName);
               }
            }
         }
      }
   }
}
