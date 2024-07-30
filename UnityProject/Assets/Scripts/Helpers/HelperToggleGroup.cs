using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity's ToggleGroup exhibits unexpected behavior when it or its Toggles are deactivated.
/// This class addresses those issues and also offers a method for programmatically activating Toggles within a ToggleGroup.
/// </summary>

namespace KenCode
{
    [RequireComponent(typeof(ToggleGroup))]
    public class HelperToggleGroup : MonoBehaviour
    {
        [Header("Configs")]

        [SerializeField]
        [Tooltip("Toggle to set as default, provided it is not inactive. Can be overwritten by ToUseLastSelectedToggle.")]
        private Toggle m_ToggleDefault;
        [SerializeField]
        [Tooltip("Uses the last selected toggle when enabled")]
        private bool m_UseLastSelectedToggle;

        // Non-Inspector Variables
        private ToggleGroup m_ToggleGroup;
        private List<Toggle> m_ListToggles;

        #region Unity Methods
        private void OnEnable()
        {
            var toggleToOn = m_UseLastSelectedToggle || m_ToggleDefault == null ? CalcFirstToggleSwitchedOn() : m_ToggleDefault;
            SetToggle(toggleToOn);
        }
        #endregion

        private void InitToggleList()
        {
            // Get List of ChildToggles under ToggleGroup
            m_ToggleGroup = GetComponent<ToggleGroup>();
            var listChildToggles = m_ToggleGroup.gameObject.GetComponentsInChildren<Toggle>(true).ToList();
            if (listChildToggles.IsEmpty())
                Log.DebugWarning("listChildToggles is empty", transform);

            // Populate m_ListToggles from
            m_ListToggles = new();
            foreach (var toggle in listChildToggles)
                if (toggle.group == m_ToggleGroup)
                    m_ListToggles.Add(toggle);
        }

        public void SetToggle(Toggle toggleToOn, bool toNotify = true)
        {
            if (m_ListToggles == null)
                InitToggleList();

            // Catch Exception
            if (!m_ListToggles.Contains(toggleToOn))
            {
                Log.DebugError($"Toggle:{toggleToOn} does not belong to ToggleGroup", transform);
                return;
            }

            bool allowSwitchOffOriginal = m_ToggleGroup.allowSwitchOff;
            m_ToggleGroup.allowSwitchOff = true;

            // Note: SetAllTogglesOff() doesnt work on InactiveToggles so need to manually loop and set them
            foreach (var toggle in m_ListToggles)
                toggle.isOn = false;

            if (toNotify)
                toggleToOn.isOn = true;
            else
                toggleToOn.SetIsOnWithoutNotify(true);

            m_ToggleGroup.allowSwitchOff = allowSwitchOffOriginal;
        }

        public void SetDefaultToggleOn(bool toNotify = true)
        {
            // Catch Exception
            if (m_ToggleDefault == null)
            {
                Log.DebugError($"m_ToggleDefault not set in GameObj:{transform.name} under ParentGameObj:{transform.root.name}", transform);
                return;
            }

            SetToggle(m_ToggleDefault, toNotify);
        }

        #region Calculation Methods
        private Toggle CalcFirstToggleSwitchedOn()
        {
            if (m_ListToggles == null)
                InitToggleList();

            // Catch Exception
            if (m_ListToggles.IsEmpty())
            {
                Log.DebugWarning("m_ListToggles is empty", transform);
                return null;
            }

            // Loop through all Toggles in List
            foreach (var toggle in m_ListToggles)
                if (toggle.isOn)
                    return toggle;

            // Return 1st Toggle in List as none are switched on
            return m_ListToggles[0];
        }
        #endregion
    }
}
