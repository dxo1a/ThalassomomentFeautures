
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(CharacterController))]
public class CharacterControllerEditor : Editor
{
    bool survivalStatsFoldout, survivalStatsMultipliersFoldout, raycastInteractFoldout = false;

    GUIStyle labelHeaderStyle; //label style
    GUIStyle BoxPanel; //vertical box style
    GUIStyle ShowMoreStyle; //button show more (color in the line)
    Texture2D BoxPanelColor; //vertical box color

    public void OnEnable()
    {
        BoxPanelColor= new Texture2D(1, 1, TextureFormat.RGBAFloat, false);;
        BoxPanelColor.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.2f));
        BoxPanelColor.Apply();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        labelHeaderStyle = labelHeaderStyle != null? labelHeaderStyle : new GUIStyle(GUI.skin.label){alignment = TextAnchor.MiddleCenter,fontStyle = FontStyle.Bold, fontSize = 13};
        ShowMoreStyle = ShowMoreStyle != null? ShowMoreStyle : new GUIStyle(GUI.skin.label){ alignment = TextAnchor.MiddleLeft, margin = new RectOffset(15,0,0,0) ,fontStyle = FontStyle.Bold, fontSize = 11, richText = true};
        BoxPanel = BoxPanel != null ? BoxPanel : new GUIStyle(GUI.skin.box){normal = {background = BoxPanelColor}};
        labelHeaderStyle.normal.textColor = Color.white;

        CharacterController characterController = (CharacterController)target;

        EditorGUILayout.Space(); EditorGUILayout.LabelField("",GUI.skin.horizontalSlider,GUILayout.MaxHeight(6)); EditorGUILayout.Space();
        
        #region Survival Stats
        GUILayout.Label("Survival Stats",labelHeaderStyle,GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical(BoxPanel);
        if(survivalStatsFoldout)
        {
            characterController.curHealth = EditorGUILayout.Slider("Current Health", characterController.curHealth, 0f, 100f);
            characterController.maxHealth = EditorGUILayout.Slider("Maximum Health", characterController.maxHealth, 0f, 100f);
            EditorGUI.BeginDisabledGroup(true);
            characterController.minHealth = EditorGUILayout.Slider("Minimum Health", characterController.minHealth, 0f, 0f);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();
            characterController.curHunger = EditorGUILayout.Slider("Current Hunger", characterController.curHunger, 0f, 100f);
            characterController.maxHunger = EditorGUILayout.Slider("Maximum Hunger", characterController.maxHunger, 0f, 100f);
            EditorGUI.BeginDisabledGroup(true);
            characterController.minHunger = EditorGUILayout.Slider("Minimum Hunger", characterController.minHunger, 0f, 0f);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();
            characterController.curOxygen = EditorGUILayout.Slider("Current Oxygen", characterController.curOxygen, 0f, 100f);
            characterController.maxOxygen = EditorGUILayout.Slider("Maximum Oxygen", characterController.maxOxygen, 0f, 100f);
            EditorGUI.BeginDisabledGroup(true);
            characterController.minOxygen = EditorGUILayout.Slider("Minimum Oxygen", characterController.minOxygen, 0f, 0f);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space(); EditorGUILayout.LabelField("",GUI.skin.horizontalSlider,GUILayout.MaxHeight(6)); EditorGUILayout.Space();
            GUILayout.Label("Multipliers",labelHeaderStyle,GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(BoxPanel);
            if(survivalStatsMultipliersFoldout)
            {
                characterController.addOxygenStep = EditorGUILayout.IntSlider("Add Oxygen Step", characterController.addOxygenStep, 0, 25);
                characterController.decreaseOxygenStep = EditorGUILayout.IntSlider("Decrease Oxygen Step", characterController.decreaseOxygenStep, 0, 25);
                characterController.addHungerStep = EditorGUILayout.IntSlider("Add Hunger Step", characterController.addHungerStep, 0, 25);
                characterController.decreaseHungerStep = EditorGUILayout.IntSlider("Decrease Hunger Step", characterController.decreaseHungerStep, 0, 25);
            }
            EditorGUILayout.Space();
            survivalStatsMultipliersFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(survivalStatsMultipliersFoldout,survivalStatsMultipliersFoldout ?  "<color=#0171BB>show less</color>" : "<color=#0171BB>show more</color>", ShowMoreStyle);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        EditorGUILayout.Space();
        survivalStatsFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(survivalStatsFoldout,survivalStatsFoldout ?  "<color=#0171BB>show less</color>" : "<color=#0171BB>show more</color>", ShowMoreStyle);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndFoldoutHeaderGroup();
        #endregion

        #region RaycastInteract
        GUILayout.Label("Raycast Interact",labelHeaderStyle,GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical(BoxPanel);
        if(raycastInteractFoldout)
        {
            characterController.interactDistance = EditorGUILayout.IntSlider("Interact Distance", characterController.interactDistance, 1, 5);
        }
        EditorGUILayout.Space();
        raycastInteractFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(raycastInteractFoldout, raycastInteractFoldout ?  "<color=#0171BB>show less</color>" : "<color=#0171BB>show more</color>", ShowMoreStyle);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndFoldoutHeaderGroup();
        #endregion
    }
}
