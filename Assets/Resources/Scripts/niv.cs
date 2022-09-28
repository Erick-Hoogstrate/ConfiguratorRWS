using UnityEngine.UI;
using UnityEngine;
using u040.prespective.standardcomponents.userinterface.buttons.encoders;
using u040.prespective.standardcomponents.userinterface.lights;

public class niv : MonoBehaviour
{
    public Text ARK;
    public Text ARK_diff;
    public Text chamber_ARK;
    public Text chamber_Waal;
    public Text Waal;
    public Text diff;

    public LinearEncoder WaalEnc;
    public LinearEncoder ARKEnc;
    public LinearEncoder kolk1Enc;
    public LinearEncoder kolk2Enc;
    public GameObject Downstream_EnteringTL_East_Q_Red;
    public GameObject Downstream_EnteringTL_East_Q_Green;
    public GameObject Downstream_EnteringTL_East_Q_Sper;

    public GameObject Downstream_LeavingTL_East_Q_Red;
    public GameObject Downstream_LeavingTL_East_Q_Green;

    public GameObject Middle_DownstreamTLs_East_Q_Red;
    public GameObject Middle_DownstreamTLs_East_Q_Green;

    public GameObject Upstream_EnteringTL_East_Q_Red;
    public GameObject Upstream_EnteringTL_East_Q_Green;
    public GameObject Upstream_EnteringTL_East_Q_Sper;


    public GameObject Upstream_LeavingTL_East_Q_Red;
    public GameObject Upstream_LeavingTL_East_Q_Green;


    public Renderer GUI_Downstream_EnteringTL_East_Q_Red;
    public Renderer GUI_Downstream_EnteringTL_East_Q_Green;
    public Renderer GUI_Downstream_EnteringTL_East_Q_Sper;

    public Renderer GUI_Downstream_LeavingTL_East_Q_Red;
    public Renderer GUI_Downstream_LeavingTL_East_Q_Green;

    public Renderer GUI_Middle_DownstreamTLs_East_Q_Red;
    public Renderer GUI_Middle_DownstreamTLs_East_Q_Green;

    public Renderer GUI_Upstream_EnteringTL_East_Q_Red;
    public Renderer GUI_Upstream_EnteringTL_East_Q_Green;
    public Renderer GUI_Upstream_EnteringTL_East_Q_Sper;


    public Renderer GUI_Upstream_LeavingTL_East_Q_Red;
    public Renderer GUI_Upstream_LeavingTL_East_Q_Green;




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        ARK.text = ((float)ARKEnc.OutputSignal / 100).ToString("F2") + " m";
        ARK_diff.text = Mathf.Abs(((float)ARKEnc.OutputSignal - (float)kolk1Enc.OutputSignal) / 100).ToString("F2") + " m";
        chamber_ARK.text = ((float)kolk1Enc.OutputSignal / 100).ToString("F2") + " m";
        chamber_Waal.text = ((float)kolk2Enc.OutputSignal / 100).ToString("F2") + " m";
        Waal.text = ((float)WaalEnc.OutputSignal / 100).ToString("F2") + " m";
        diff.text = Mathf.Abs(((float)WaalEnc.OutputSignal - (float)kolk1Enc.OutputSignal) / 100).ToString("F2") + " m";

        GUI_Downstream_EnteringTL_East_Q_Red.enabled = true;
        GUI_Downstream_LeavingTL_East_Q_Red.enabled = true;
        GUI_Middle_DownstreamTLs_East_Q_Red.enabled = true;
        GUI_Upstream_EnteringTL_East_Q_Red.enabled = true;
        GUI_Upstream_LeavingTL_East_Q_Red.enabled = true;


        if (Downstream_EnteringTL_East_Q_Red.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Downstream_EnteringTL_East_Q_Red.enabled = true;
        }
        if (Downstream_EnteringTL_East_Q_Red.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Downstream_EnteringTL_East_Q_Red.enabled = false;
        }
        if (Downstream_EnteringTL_East_Q_Green.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Downstream_EnteringTL_East_Q_Green.enabled = true;

        }
        if (Downstream_EnteringTL_East_Q_Green.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Downstream_EnteringTL_East_Q_Green.enabled = false;
        }
        if (Downstream_EnteringTL_East_Q_Sper.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Downstream_EnteringTL_East_Q_Sper.enabled = true;
        }
        if (Downstream_EnteringTL_East_Q_Sper.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Downstream_EnteringTL_East_Q_Sper.enabled = false;
        }



        if (Downstream_LeavingTL_East_Q_Red.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Downstream_LeavingTL_East_Q_Red.enabled = true;
        }
        if (Downstream_LeavingTL_East_Q_Red.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Downstream_LeavingTL_East_Q_Red.enabled = false;
        }

        if (Downstream_LeavingTL_East_Q_Green.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Downstream_LeavingTL_East_Q_Green.enabled = true;
        }
        if (Downstream_LeavingTL_East_Q_Green.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Downstream_LeavingTL_East_Q_Green.enabled = false;
        }



        if (Middle_DownstreamTLs_East_Q_Red.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Middle_DownstreamTLs_East_Q_Red.enabled = true;
        }
        if (Middle_DownstreamTLs_East_Q_Red.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Middle_DownstreamTLs_East_Q_Red.enabled = false;
        }

        if (Middle_DownstreamTLs_East_Q_Green.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Middle_DownstreamTLs_East_Q_Green.enabled = true;
        }
        if (Middle_DownstreamTLs_East_Q_Green.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Middle_DownstreamTLs_East_Q_Green.enabled = false;
        }



        if (Upstream_EnteringTL_East_Q_Red.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Upstream_EnteringTL_East_Q_Red.enabled = true;
        }
        if (Upstream_EnteringTL_East_Q_Red.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Upstream_EnteringTL_East_Q_Red.enabled = false;
        }

        if (Upstream_EnteringTL_East_Q_Green.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Upstream_EnteringTL_East_Q_Green.enabled = true;
        }
        if (Upstream_EnteringTL_East_Q_Green.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Upstream_EnteringTL_East_Q_Green.enabled = false;
        }

        if (Upstream_EnteringTL_East_Q_Sper.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Upstream_EnteringTL_East_Q_Sper.enabled = true;
        }
        if (Upstream_EnteringTL_East_Q_Sper.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Upstream_EnteringTL_East_Q_Sper.enabled = false;
        }



        if (Upstream_LeavingTL_East_Q_Red.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Upstream_LeavingTL_East_Q_Red.enabled = false;
        }
        if (Upstream_LeavingTL_East_Q_Red.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Upstream_LeavingTL_East_Q_Red.enabled = true;
        }
        if (Upstream_LeavingTL_East_Q_Green.GetComponent<IndicatorLight>().IsActive == true)
        {
            GUI_Upstream_LeavingTL_East_Q_Green.enabled = true;
        }
        if (Upstream_LeavingTL_East_Q_Green.GetComponent<IndicatorLight>().IsActive == false)
        {
            GUI_Upstream_LeavingTL_East_Q_Green.enabled = false;
        }
       


    }

}

