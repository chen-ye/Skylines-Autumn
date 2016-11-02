﻿using System;
using System.Collections.Generic;
using System.Linq;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace DynamicFoliage.OptionsSpace.OptionsFramework
{
    public static class UIHelperBaseExtension
    {
        public static IEnumerable<UIComponent> AddOptionsGroup<T>(this UIHelperBase helper) where T : IModOptions
        {
            var result = new List<UIComponent>();
            var properties = (from property in typeof(T).GetProperties() select property.Name).Where(name => name != "FileName");
            var groups = new Dictionary<string, UIHelperBase>();
            foreach (var propertyName in properties)
            {
                var description = OptionsWrapper<T>.Options.GetPropertyDescription(propertyName);
                var groupName = OptionsWrapper<T>.Options.GetPropertyGroup(propertyName);
                if (groupName == null)
                {
                    var component = helper.ProcessProperty<T>(propertyName, description);
                    if (component != null)
                    {
                        result.Add(component);
                    }
                }
                else
                {
                    if (!groups.ContainsKey(groupName))
                    {
                        groups[groupName] = helper.AddGroup(groupName);
                    }
                    var component = groups[groupName].ProcessProperty<T>(propertyName, description);
                    if (component != null)
                    {
                        result.Add(component);
                    }
                }
            }
            return result;
        }

        private static UIComponent ProcessProperty<T>(this UIHelperBase group, string name, string description) where T : IModOptions
        {
            if (OptionsWrapper<T>.Options.IsCheckbox(name))
            {
                return group.AddCheckbox<T>(description, name, OptionsWrapper<T>.Options.GetCheckboxAction(name));
            }
            if (OptionsWrapper<T>.Options.IsTextField(name))
            {
                return group.AddTextField<T>(description, name, OptionsWrapper<T>.Options.GetTextFieldAction(name));
            }
            if (OptionsWrapper<T>.Options.IsDropdown(name))
            {
                return group.AddDropdown<T>(description, name, OptionsWrapper<T>.Options.GetDropdownItems(name), OptionsWrapper<T>.Options.GetDropdownAction(name));
            }
            if (OptionsWrapper<T>.Options.IsDynamicDropdown(name))
            {
                return group.AddDynamicDropdown<T>(description, name, OptionsWrapper<T>.Options.GetDynamicDropdownItems(name), OptionsWrapper<T>.Options.GetDynamicDropdownAction(name));
            }
            //TODO: more control types
            return null;
        }

        private static UIDropDown AddDropdown<T>(this UIHelperBase group, string text, string propertyName, IList<KeyValuePair<string, int>> items, Action<int> action) where T : IModOptions
        {
            var property = typeof(T).GetProperty(propertyName);
            var defaultCode = (int)property.GetValue(OptionsWrapper<T>.Options, null);
            int defaultSelection;
            try
            {
                defaultSelection = items.First(kvp => kvp.Value == defaultCode).Value;
            } catch {
                defaultSelection = 0;
                property.SetValue(OptionsWrapper<T>.Options, items.First().Value, null);
            }
            return (UIDropDown) group.AddDropdown(text, items.Select(kvp => kvp.Key).ToArray(), defaultSelection, sel =>
            {
                var code = items[sel].Value;
                property.SetValue(OptionsWrapper<T>.Options, code, null);
                OptionsWrapper<T>.SaveOptions();
                action.Invoke(code);
            });
        }

        private static UIDropDown AddDynamicDropdown<T>(this UIHelperBase group, string text, string propertyName, IList<string> items, Action<string> action) where T : IModOptions
        {
            var property = typeof(T).GetProperty(propertyName);
            var defaultString = (string)property.GetValue(OptionsWrapper<T>.Options, null);
            int defaultSelection;
            try
            {
                defaultSelection = items.IndexOf(defaultString);
            }
            catch
            {
                defaultSelection = 0;
                property.SetValue(OptionsWrapper<T>.Options, items[0], null);
            }

            Debug.Log("Option Items");
            Debug.Log(items.ToString());

            return (UIDropDown)group.AddDropdown(text, items.ToArray(), defaultSelection, sel =>
            {
                var selection = items[sel];
                property.SetValue(OptionsWrapper<T>.Options, selection, null);
                OptionsWrapper<T>.SaveOptions();
                action.Invoke(selection);
            });
        }

        private static UICheckBox AddCheckbox<T>(this UIHelperBase group, string text, string propertyName, Action<bool> action) where T : IModOptions
        {
            var property = typeof(T).GetProperty(propertyName);
            return (UICheckBox)group.AddCheckbox(text, (bool)property.GetValue(OptionsWrapper<T>.Options, null),
                b =>
                {
                    property.SetValue(OptionsWrapper<T>.Options, b, null);
                    OptionsWrapper<T>.SaveOptions();
                    action.Invoke(b);
                });
        }

        private static UITextField AddTextField<T>(this UIHelperBase group, string text, string propertyName, Action<string> action) where T : IModOptions
        {
            var property = typeof(T).GetProperty(propertyName);
            var initialValue = Convert.ToString(property.GetValue(OptionsWrapper<T>.Options, null));
            return (UITextField)group.AddTextfield(text, initialValue, s => { },
                s =>
                {
                    object value;
                    if (property.PropertyType == typeof(int))
                    {
                        value = Convert.ToInt32(s);
                    }
                    else if (property.PropertyType == typeof(short))
                    {
                        value = Convert.ToInt16(s);
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        value = Convert.ToDouble(s);
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        value = Convert.ToSingle(s);
                    }
                    else
                    {
                        value = s; //TODO: more types
                    }
                    property.SetValue(OptionsWrapper<T>.Options, value, null);
                    OptionsWrapper<T>.SaveOptions();
                    action.Invoke(s);
                });
        }
    }
}