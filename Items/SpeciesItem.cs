﻿using NewHorizons.Utility;
using UnityEngine;

namespace FifthModJam
{
    /// <summary>
    /// A CustomItem associated with a Species component.
    /// Used for the diorama door puzzle and the Nomai beam puzzle.
    /// </summary>
    [RequireComponent(typeof(SpeciesTypeData))]
    public class SpeciesItem : CustomItem
    {
        // This is for solving the diorama door puzzle, as well as the nomai beam in the exhibit
        private SpeciesEnum _species;
        public SpeciesEnum Species => _species;

        protected override void VerifyUnityParameters()
        {
            base.VerifyUnityParameters();

            var speciesData = this.GetComponent<SpeciesTypeData>();
            if (speciesData == null || speciesData.Species == SpeciesEnum.INVALID)
            {
                FifthModJam.WriteLine("[SpeciesItem] speciesData null or invalid", OWML.Common.MessageType.Error);
            }
            else
            {
                _species = speciesData.Species;
            }
        }

        public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
        {
            // [Note to self@Stache: I don't know what this does, since it seems it gets overwritten by base.DropItem()'s own SetSector anyway? To investigate]
            if (_species == SpeciesEnum.NOMAI)
            {
                Sector desiredSector;
                if (Locator.GetPlayerSectorDetector().IsWithinSector("ScaledMuseum"))
                {
                    desiredSector = SearchUtilities.Find("ScaledMuseum_Body/Sector").GetComponent<Sector>();
                }
                else
                {
                    desiredSector = SearchUtilities.Find("OminousOrbiter_Body/Sector").GetComponent<Sector>();
                }
                this.SetSector(desiredSector);
            }

            base.DropItem(position, normal, parent, sector, customDropTarget);
        }
    }
}
