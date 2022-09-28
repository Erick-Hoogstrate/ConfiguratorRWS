using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using u040.prespective.standardcomponents.userinterface.buttons.switches;
using u040.prespective.standardcomponents.userinterface.buttons.encoders;

public class bd : MonoBehaviour
{
    public bool bin_lock;
    public bool mid_lock;
    public bool bui_lock;
    public bool inner_lock_is_closing;
    public bool outer_lock_is_closing;
    public bool outer_gate_is_moving;
    public bool outer_gate_is_open;

    public bool bool_stop_lock;
    public bool inner_gate_is_moving;
    public bool middle_gate_is_moving;

    public bool inner_lock_is_opening;
    public bool outer_lock_is_opening;
    public bool blink_inner_open;
    public bool blink_inner_closed;
    public bool blink_middle_open;
    public bool blink_middle_closed;
    public bool blink_outer_open;
    public bool blink_outer_closed;
    public bool button;
    public bool state;
    public bool middle_lock_is_closing;
    public bool middle_lock_is_opening;
    public bool toggle_inner_gate_open;
    public bool toggle_inner_gate_close;
    public bool toggle_inner_paddle_open;
    public bool toggle_inner_paddle_close;
    public bool toggle_inner_gate_stop;
    public bool toggle_stop_inner_lock;
    public bool toggle_stop_middle_lock;
    public bool toggle_stop_outer_lock;
    public bool toggleEL_ARK_red;
    public bool toggleEL_ARK_redgreen;
    public bool toggleEL_ARK_green;
    public bool toggleEL_ARK_blocked;
    public bool toggleEL_Waal_red;
    public bool toggleEL_Waal_redgreen;
    public bool toggleEL_Waal_green;
    public bool toggleEL_Waal_blocked;
    public bool toggleLL_ARK_red;
    public bool toggleLL_ARK_green;
    public bool toggleLL_Waal_red;
    public bool toggleLL_Waal_green;
    public bool toggleLL_mid_red;
    public bool toggleLL_mid_green;



    public bool toggle_middle_gate_open;
    public bool toggle_middle_gate_close;
    public bool toggle_middle_paddle_open;
    public bool toggle_middle_paddle_close;
    public bool toggle_outer_gate_open;
    public bool toggle_outer_gate_close;
    public bool toggle_outer_paddle_open;
    public bool toggle_outer_paddle_close;



    public GameObject bin_lock_1;
    public GameObject bin_lock_2;

    public GameObject middle_gate_east;
    public GameObject middle_gate_west;

    public GameObject outergate_open;
    public GameObject middlegate_closed;
    public GameObject middlegate_open;


    public GameObject innergate_open;
    public GameObject innergate_closed;

    public GameObject middenhoofd_toggle;
    public GameObject mid_close_lock;
    public GameObject mid_block;
    public GameObject mid_open_lock;
    public GameObject mid_close_paddle;
    public GameObject mid_open_paddle;
    public GameObject mid_stop_paddle;
    public GameObject mid_stop_lock;
    public GameObject bin_close_paddle;
    public GameObject bin_open_paddle;
    private Camera MainCamera;

    public Material GUI_sluis_open_donker;
    public Material GUI_sluis_open_licht;
    public Material GUI_sluis_dicht_donker;
    public Material GUI_sluis_dicht_licht;
    public Material GUI_arrow_active;
    public Material GUI_arrow_inactive;
    public Material GUI_sluis_close_donker;
    public Material GUI_sluis_close_licht;

    public Material interval;
    public Material open_state;
    public Material close_state;
    public Renderer ren_bin_lock_1a;
    public Renderer ren_bin_lock_1b;
    public Renderer ren_bin_lock_2a;
    public Renderer ren_bin_lock_2b;

    public Renderer ren_middle_lock_east_a;
    public Renderer ren_middle_lock_east_b;
    public Renderer ren_middle_lock_west_a;
    public Renderer ren_middle_lock_west_b;

    public Renderer ren_bin_open_lock;
    public Renderer ren_bin_close_lock;
    public Renderer ren_bin_close_paddle;
    public Renderer ren_bin_open_paddle;

    public Renderer ren_mid_open_lock;
    public Renderer ren_mid_close_lock;
    public Renderer ren_mid_close_paddle;
    public Renderer ren_mid_open_paddle;

    public Renderer ren_bui_open_lock;
    public Renderer ren_bui_close_lock;
    public Renderer ren_bui_close_paddle;
    public Renderer ren_bui_open_paddle;

    public Renderer ren_outer_lock;

    public GameObject ren_EL_ARK_red;
    public GameObject ren_EL_ARK_redgreen;
    public GameObject ren_EL_ARK_green;
    public GameObject ren_EL_ARK_blocked;
    public GameObject ren_EL_Waal_red;
    public GameObject ren_EL_Waal_redgreen;
    public GameObject ren_EL_Waal_green;
    public GameObject ren_EL_Waal_blocked;
    public GameObject ren_LL_ARK_red;
    public GameObject ren_LL_ARK_green;
    public GameObject ren_LL_Waal_red;
    public GameObject ren_LL_Waal_green;
    public GameObject ren_LL_mid_red;
    public GameObject ren_LL_mid_green;


    public GameObject innergate_closed_sensor;
    public GameObject innergate_open_sensor;
    public GameObject middlegate_closed_sensor;
    public GameObject middlegate_open_sensor;
    public GameObject outergate_closed_sensor;
    public GameObject outergate_open_sensor;
    public GameObject outergate_closed;







    IEnumerator blinking_inner_open()
    {
        blink_inner_open = false;

        while (inner_gate_is_moving)
        {
            ren_bin_lock_1a.material = interval;
            ren_bin_lock_1b.material = interval;
            ren_bin_lock_2a.material = interval;
            ren_bin_lock_2b.material = interval;
            yield return new WaitForSeconds(0.7f);
            ren_bin_lock_1a.material = close_state;
            ren_bin_lock_1b.material = close_state;
            ren_bin_lock_2a.material = close_state;
            ren_bin_lock_2b.material = close_state;
            yield return new WaitForSeconds(0.7f);

        }
    }

    IEnumerator blinking_inner_close()
    {
        blink_inner_closed = false;

        while (inner_gate_is_moving)
        {
            ren_bin_lock_1a.material = interval;
            ren_bin_lock_1b.material = interval;
            ren_bin_lock_2a.material = interval;
            ren_bin_lock_2b.material = interval;
            yield return new WaitForSeconds(0.7f);
            ren_bin_lock_1a.material = open_state;
            ren_bin_lock_1b.material = open_state;
            ren_bin_lock_2a.material = open_state;
            ren_bin_lock_2b.material = open_state;
            yield return new WaitForSeconds(0.7f);

        }
    }

    IEnumerator blinking_middle_open()
    {
        blink_middle_open = false;

        while (middle_gate_is_moving)
        {
            ren_middle_lock_east_a.material = close_state;
            ren_middle_lock_east_b.material = close_state;
            ren_middle_lock_west_a.material = close_state;
            ren_middle_lock_west_b.material = close_state;
            yield return new WaitForSeconds(0.7f);
            ren_middle_lock_east_a.material = interval;
            ren_middle_lock_east_b.material = interval;
            ren_middle_lock_west_a.material = interval;
            ren_middle_lock_west_b.material = interval;
            yield return new WaitForSeconds(0.7f);

        }
    }

    IEnumerator blinking_middle_close()
    {
        blink_middle_closed = false;

        while (middle_gate_is_moving)
        {

            ren_middle_lock_east_a.material = open_state;
            ren_middle_lock_east_b.material = open_state;
            ren_middle_lock_west_a.material = open_state;
            ren_middle_lock_west_b.material = open_state;
            yield return new WaitForSeconds(0.7f);
            ren_middle_lock_east_a.material = interval;
            ren_middle_lock_east_b.material = interval;
            ren_middle_lock_west_a.material = interval;
            ren_middle_lock_west_b.material = interval;
            yield return new WaitForSeconds(0.7f);

        }
    }

    IEnumerator blinking_outer_open()
    {
        blink_outer_open = false;

        while (outer_gate_is_moving)
        {
            ren_outer_lock.material = close_state;
            yield return new WaitForSeconds(0.7f);
            ren_outer_lock.material = interval;
            yield return new WaitForSeconds(0.7f);

        }
    }

    IEnumerator blinking_outer_close()
    {
        blink_outer_closed = false;

        while (outer_gate_is_moving)
        {

            ren_outer_lock.material = open_state;
            yield return new WaitForSeconds(0.7f);
            ren_outer_lock.material = interval;
            yield return new WaitForSeconds(0.7f);

        }
    }
    void Start()
    {
        ren_EL_ARK_red.GetComponent<Renderer>().enabled = false;
        ren_EL_ARK_redgreen.GetComponent<Renderer>().enabled = false;
        ren_EL_ARK_green.GetComponent<Renderer>().enabled = false;
        ren_EL_ARK_blocked.GetComponent<Renderer>().enabled = false;
        ren_EL_Waal_red.GetComponent<Renderer>().enabled = false;
        ren_EL_Waal_redgreen.GetComponent<Renderer>().enabled = false;
        ren_EL_Waal_green.GetComponent<Renderer>().enabled = false;
        ren_EL_Waal_blocked.GetComponent<Renderer>().enabled = false;
        ren_LL_ARK_red.GetComponent<Renderer>().enabled = false;
        ren_LL_ARK_green.GetComponent<Renderer>().enabled = false;
        ren_LL_Waal_red.GetComponent<Renderer>().enabled = false;
        ren_LL_Waal_green.GetComponent<Renderer>().enabled = false;
        ren_LL_mid_red.GetComponent<Renderer>().enabled = false;
        ren_LL_mid_green.GetComponent<Renderer>().enabled = false;

        outer_gate_is_moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera = Camera.main;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out Hit))
            {
                if (Hit.transform.name == "middenhoofd_toggle")
                {
                    mid_block.GetComponent<Renderer>().enabled = false;
                    mid_close_lock.GetComponent<Renderer>().enabled = false;
                    mid_open_lock.GetComponent<Renderer>().enabled = false;
                    mid_close_paddle.GetComponent<Renderer>().enabled = false;
                    mid_open_paddle.GetComponent<Renderer>().enabled = false;
                    mid_stop_lock.GetComponent<Renderer>().enabled = false;
                    mid_stop_paddle.GetComponent<Renderer>().enabled = false;

                    button = !button; // toggles onoff at each click


                }

                if (Hit.transform.name == "bin_open_lock")
                {
                    toggle_inner_gate_open = !toggle_inner_gate_open;

                    if (toggle_inner_gate_open)
                    {
                        ren_bin_open_lock.material = GUI_sluis_open_donker;
                    }
                    else
                    {
                        ren_bin_open_lock.material = GUI_sluis_open_licht;

                    }
                }


                if (Hit.transform.name == "bin_close_lock")
                {
                    toggle_inner_gate_close = !toggle_inner_gate_close;
                    


                    if (toggle_inner_gate_close)
                    {
                        ren_bin_close_lock.material = GUI_sluis_close_donker;
                    }
                    else
                    {
                        ren_bin_close_lock.material = GUI_sluis_close_licht;
                    }
                }



                if (Hit.transform.name == "bin_open_paddle")
                {
                    toggle_inner_paddle_open = !toggle_inner_paddle_open;
                    

                    if (toggle_inner_paddle_open)
                    {
                        ren_bin_open_paddle.material = GUI_arrow_active;
                    }
                    else
                    {
                        ren_bin_open_paddle.material = GUI_arrow_inactive;

                    }


                }

                if (Hit.transform.name == "bin_close_paddle")
                {
                    toggle_inner_paddle_close = !toggle_inner_paddle_close;

                    if (toggle_inner_paddle_close)
                    {
                        ren_bin_close_paddle.material = GUI_arrow_active;
                    }
                    else
                    {
                        ren_bin_close_paddle.material = GUI_arrow_inactive;

                    }
                }

                if (Hit.transform.name == "bin_stop_lock")
                {
                    toggle_stop_inner_lock = !toggle_stop_inner_lock;

                    if (toggle_inner_paddle_close)
                    {
                        inner_lock_is_closing = false;
                        inner_lock_is_opening = false;
                        inner_gate_is_moving = false;
                        bin_lock = false;
                        ren_bin_lock_1a.material = interval;
                        ren_bin_lock_1b.material = interval;
                        ren_bin_lock_2a.material = interval;
                        ren_bin_lock_2b.material = interval;
                    }
                }

                if (Hit.transform.name == "mid_open_lock")
                {
                    toggle_middle_gate_open = !toggle_middle_gate_open;

                    if (toggle_middle_gate_open)
                    {
                        ren_mid_open_lock.material = GUI_sluis_open_donker;
                    }
                    else
                    {
                        ren_mid_open_lock.material = GUI_sluis_open_licht;

                    }
                }


                if (Hit.transform.name == "mid_close_lock")
                {
                    toggle_middle_gate_close = !toggle_middle_gate_close;


                    if (toggle_middle_gate_close)
                    {
                        ren_mid_close_lock.material = GUI_sluis_close_donker;
                    }
                    else
                    {
                        ren_mid_close_lock.material = GUI_sluis_close_licht;
                    }
                }



                if (Hit.transform.name == "mid_open_paddle")
                {
                    toggle_middle_paddle_open = !toggle_middle_paddle_open;

                    if (toggle_middle_paddle_open)
                    {
                        ren_mid_open_paddle.material = GUI_arrow_active;
                    }
                    else
                    {
                        ren_mid_open_paddle.material = GUI_arrow_inactive;

                    }


                }

                if (Hit.transform.name == "mid_close_paddle")
                {
                    toggle_middle_paddle_close = !toggle_middle_paddle_close;

                    if (toggle_middle_paddle_close)
                    {
                        ren_mid_close_paddle.material = GUI_arrow_active;
                    }
                    else
                    {
                        ren_mid_close_paddle.material = GUI_arrow_inactive;

                    }
                }

                if (Hit.transform.name == "mid_stop_lock")
                {
                    toggle_stop_middle_lock = !toggle_stop_middle_lock;

                    if (toggle_middle_paddle_close)
                    {
                        middle_lock_is_closing = false;
                        middle_lock_is_opening = false;
                        middle_gate_is_moving = false;
                        mid_lock = false;
                        ren_middle_lock_east_a.material = interval;
                        ren_middle_lock_east_b.material = interval;
                        ren_middle_lock_west_a.material = interval;
                        ren_middle_lock_west_b.material = interval;
                    }

                }
                if (Hit.transform.name == "bui_open_lock")
                {
                    toggle_outer_gate_open = !toggle_outer_gate_open;
                    //StartCoroutine("blinking_outer_open");
                    

                    if (toggle_outer_gate_open)
                    {
                        ren_bui_open_lock.material = GUI_arrow_active;
                        
                    }
                    else
                    {
                        ren_bui_open_lock.material = GUI_arrow_inactive;

                    }
                }


                if (Hit.transform.name == "bui_close_lock")
                {
                    toggle_outer_gate_close = !toggle_outer_gate_close;


                    if (toggle_outer_gate_close)
                    {
                        ren_bui_close_lock.material = GUI_arrow_active;
                    }
                    else
                    {
                        ren_bui_close_lock.material = GUI_arrow_inactive;
                    }
                }



                if (Hit.transform.name == "bui_open_paddle")
                {
                    toggle_outer_paddle_open = !toggle_outer_paddle_open;
                    outer_gate_is_moving = true;

                    if (toggle_outer_paddle_open)
                    {
                        ren_bui_open_paddle.material = GUI_arrow_active;
                    }
                    else
                    {
                        ren_bui_open_paddle.material = GUI_arrow_inactive;

                    }


                }

                if (Hit.transform.name == "bui_close_paddle")
                {
                    toggle_outer_paddle_close = !toggle_outer_paddle_close;

                    if (toggle_outer_paddle_close)
                    {
                        ren_bui_close_paddle.material = GUI_arrow_active;
                    }
                    else
                    {
                        ren_bui_close_paddle.material = GUI_arrow_inactive;

                    }
                }

                if (Hit.transform.name == "bui_stop_lock")
                {
                    toggle_stop_outer_lock = !toggle_stop_outer_lock;

                    if (toggle_outer_paddle_close)
                    {
                        outer_lock_is_closing = false;
                        outer_lock_is_opening = false;
                        outer_gate_is_moving = false;
                        bui_lock = false;
                        ren_outer_lock.material = interval;

                    }




                }

                if (Hit.transform.name == "red_ARK")
                {
                    toggleEL_ARK_red = !toggleEL_ARK_red;

                    if (toggleEL_ARK_red)
                    {
                        ren_EL_ARK_red.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_EL_ARK_red.GetComponent<Renderer>().enabled = false;

                    }
                }

                if (Hit.transform.name == "redgreen_ARK")
                {
                    toggleEL_ARK_redgreen = !toggleEL_ARK_redgreen;

                    if (toggleEL_ARK_redgreen)
                    {
                        ren_EL_ARK_redgreen.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_EL_ARK_redgreen.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "green_ARK")
                {
                    toggleEL_ARK_green = !toggleEL_ARK_green;

                    if (toggleEL_ARK_green)
                    {
                        ren_EL_ARK_green.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_EL_ARK_green.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "red_WAAL")
                {
                    toggleEL_Waal_red = !toggleEL_Waal_red;

                    if (toggleEL_Waal_red)
                    {
                        ren_EL_Waal_red.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_EL_Waal_red.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "redgreen_WAAL")
                {
                    toggleEL_Waal_redgreen = !toggleEL_Waal_redgreen;

                    if (toggleEL_Waal_redgreen)
                    {
                        ren_EL_Waal_redgreen.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_EL_Waal_redgreen.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "green_WAAL")
                {
                    toggleEL_Waal_green = !toggleEL_Waal_green;

                    if (toggleEL_Waal_green)
                    {
                        ren_EL_Waal_green.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_EL_Waal_green.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "green_ARK_L")
                {

                    toggleLL_ARK_green = !toggleLL_ARK_green;

                    if (toggleLL_ARK_green)
                    {
                        ren_LL_ARK_green.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_LL_ARK_green.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "red_ARK_L")
                {
                    toggleLL_ARK_red = !toggleLL_ARK_red;

                    if (toggleLL_ARK_red)
                    {
                        ren_LL_ARK_red.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_LL_ARK_red.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "red_mid")
                {
                    toggleLL_mid_red = !toggleLL_mid_red;

                    if (toggleLL_mid_red)
                    {
                        ren_LL_mid_red.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_LL_mid_red.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "green_mid")
                {
                    toggleLL_mid_green = !toggleLL_mid_green;

                    if (toggleLL_mid_green)
                    {
                        ren_LL_mid_green.GetComponent<Renderer>().enabled = true;

                    }
                    else
                    {
                        ren_LL_mid_green.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "green_Waal_L")
                {
                    toggleLL_Waal_green = !toggleLL_Waal_green;

                    if (toggleLL_Waal_green)
                    {
                        ren_LL_Waal_green.GetComponent<Renderer>().enabled = true;

                    }
                    else
                    {
                        ren_LL_Waal_green.GetComponent<Renderer>().enabled = false;

                    }
                }
                if (Hit.transform.name == "red_Waal_L")
                {
                    toggleLL_Waal_red = !toggleLL_Waal_red;

                    if (toggleLL_Waal_red)
                    {
                        ren_LL_Waal_red.GetComponent<Renderer>().enabled = true;
                    }
                    else
                    {
                        ren_LL_Waal_red.GetComponent<Renderer>().enabled = false;

                    }
                }
            }
        }

        if (innergate_open.GetComponent<GateState>().Boolean == true)
        {
            inner_gate_is_moving = true;
            blink_inner_open = true;
            inner_lock_is_opening = true;

        }


        if (innergate_open_sensor.GetComponent<RotarySwitch>().SelectedState.Id == 0)
        {
            inner_gate_is_moving = false;
            

            ren_middle_lock_east_a.material = open_state;
            ren_middle_lock_east_b.material = open_state;
            ren_middle_lock_west_a.material = open_state;
            ren_middle_lock_west_b.material = open_state;

        }

        //if (innergate_closed.GetComponent<GateState>().Boolean == true)
        //{

        //    inner_gate_is_moving = true;

        //    blink_inner_closed = true;

        //}

        if (innergate_closed_sensor.GetComponent<RotarySwitch>().SelectedState.Id == 0)
        {
            //inner_gate_is_moving = false;
            //inner_lock_is_closing = false;
            ren_bin_lock_1a.material = close_state;
            ren_bin_lock_1b.material = close_state;
            ren_bin_lock_2a.material = close_state;
            ren_bin_lock_2b.material = close_state;

        }

        //if (outergate_open.GetComponent<GateState>().Boolean == true)
        //{
        //    outer_gate_is_moving = true;
        //    blink_outer_open = true;
        //    outer_lock_is_opening = true;

        //}


        //if (outergate_open_sensor.GetComponent<SlideSwitch>().SelectedState.Id == 0)
        //{
        //    outer_gate_is_moving = false;
        //    outer_lock_is_closing = false;

        //    ren_outer_lock.material = open_state;
        //}

        //if (outergate_closed.GetComponent<GateState>().Boolean == true)
        //{
        //    outer_gate_is_moving = true;
        //    outer_lock_is_closing = true;
        //    blink_outer_closed = true;

        //}


        //if (outergate_closed_sensor.GetComponent<SlideSwitch>().SelectedState.Id == 1)
        //{
        //    outer_gate_is_moving = false;
        //    outer_lock_is_opening = false;
        //    ren_outer_lock.material = close_state;

        //}





        //if (middlegate_closed.GetComponent<GateState>().Boolean == true)
        //{

        //    middle_gate_is_moving = true;
        //    middle_lock_is_closing = true;
        //    blink_middle_closed = true;

        //}

        //if (middlegate_closed_sensor.GetComponent<RotarySwitch>().SelectedState.Id == 0)
        //{
        //    middle_gate_is_moving = false;
        //    middle_lock_is_opening = false;
        //    ren_middle_lock_east_a.material = close_state;
        //    ren_middle_lock_east_b.material = close_state;
        //    ren_middle_lock_west_a.material = close_state;
        //    ren_middle_lock_west_b.material = close_state;

        //}

        //if (middlegate_open.GetComponent<GateState>().Boolean == true)
        //{
        //    middle_gate_is_moving = true;
        //    blink_middle_open = true;
        //    middle_lock_is_opening = true;

        //}


        //if (middlegate_open_sensor.GetComponent<RotarySwitch>().SelectedState.Id == 0)
        //{
        //    middle_gate_is_moving = false;
        //    middle_lock_is_closing = false;

        //    ren_middle_lock_east_a.material = open_state;
        //    ren_middle_lock_east_b.material = open_state;
        //    ren_middle_lock_west_a.material = open_state;
        //    ren_middle_lock_west_b.material = open_state;

        //}

        //if (middlegate_closed.GetComponent<GateState>().Boolean == true)
        //{

        //    middle_gate_is_moving = true;
        //    middle_lock_is_closing = true;
        //    blink_middle_closed = true;

        //}

        //if (middlegate_closed_sensor.GetComponent<RotarySwitch>().SelectedState.Id == 0)
        //{
        //    middle_gate_is_moving = false;
        //    middle_lock_is_opening = false;
        //    ren_middle_lock_east_a.material = close_state;
        //    ren_middle_lock_east_b.material = close_state;
        //    ren_middle_lock_west_a.material = close_state;
        //    ren_middle_lock_west_b.material = close_state;
        //}
        if (Input.GetKeyDown("up"))
        {
            outer_gate_is_moving = false;
            outer_lock_is_closing = false;

            ren_outer_lock.material = open_state;
        }
        if (Input.GetKeyDown("left"))
        {
            inner_lock_is_closing = true;
            inner_lock_is_opening = false;
            blink_inner_closed = true;
            inner_gate_is_moving = true;




        }
        if (Input.GetKeyDown("right"))
        {
            inner_lock_is_closing = false;
            ren_outer_lock.material = close_state;
            inner_gate_is_moving = false;


        }









        if (inner_lock_is_closing)
        {
            bin_lock_1.transform.rotation = Quaternion.RotateTowards(bin_lock_1.transform.rotation, Quaternion.Euler(-29.0f, 0, -70.0f), 10.0f * Time.deltaTime);
            bin_lock_2.transform.rotation = Quaternion.RotateTowards(bin_lock_2.transform.rotation, Quaternion.Euler(-29.0f, 0, 70.0f), 10.0f * Time.deltaTime);
            
            StartCoroutine("blinking_inner_close");
            

        }
        if (inner_lock_is_opening)
        {

            bin_lock_1.transform.rotation = Quaternion.RotateTowards(bin_lock_1.transform.rotation, Quaternion.Euler(-29.0f, 0, 0), 10.0f * Time.deltaTime);
            bin_lock_2.transform.rotation = Quaternion.RotateTowards(bin_lock_2.transform.rotation, Quaternion.Euler(-29.0f, 0, 0), 10.0f * Time.deltaTime);
            if (blink_inner_open)
            {
                StartCoroutine("blinking_inner_open");
            }

        }

        if (middle_lock_is_closing)
        {
            middle_gate_east.transform.rotation = Quaternion.RotateTowards(bin_lock_1.transform.rotation, Quaternion.Euler(-29.0f, 0, -70.0f), 10.0f * Time.deltaTime);
            middle_gate_west.transform.rotation = Quaternion.RotateTowards(bin_lock_2.transform.rotation, Quaternion.Euler(-29.0f, 0, 70.0f), 10.0f * Time.deltaTime);
            if (blink_middle_closed)
            {
                StartCoroutine("blinking_middle_open");
            }

        }
        if (middle_lock_is_opening)
        {
            middle_gate_east.transform.rotation = Quaternion.RotateTowards(bin_lock_1.transform.rotation, Quaternion.Euler(-29.0f, 0, 0), 10.0f * Time.deltaTime);
            middle_gate_west.transform.rotation = Quaternion.RotateTowards(bin_lock_2.transform.rotation, Quaternion.Euler(-29.0f, 0, 0), 10.0f * Time.deltaTime);
            if (blink_middle_open)
            {
                StartCoroutine("blinking_middle_close");
            }
        }
        if (outer_lock_is_opening)
        {
            if (blink_middle_open)
            {
                StartCoroutine("blinking_outer_open");
            }

        }
        if (outer_lock_is_closing)
        {
            if (blink_middle_open)
            {
                StartCoroutine("blinking_outer_close");
            }

        }










    }





        
    
}

