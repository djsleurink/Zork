﻿using Zork.Objects;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using Zork.Texts;

namespace Zork.Characters
{
    public class NPC : Character
    {
        public bool IsMurderer = false;

        public const int MinTurnsBetweenMoves = 2;
        public const int MaxTurnsBetweenMoves = 5;

        private int _turnsUntilNextMove;

        private TextTree _text;

        public TextTree Text
        {
            get { return _text; }
            protected set { _text = value; }
        }

        public NPC(string name, string description, int strength, int startHealth, Weapon weapon = null, bool isMurderer = false) : this(name, description, strength, startHealth, startHealth, weapon)
        {
        }

        public NPC(string name, string description, int strength, int startHealth, int maxHealth, Weapon weapon = null, bool isMurderer = false) : base(name, description, strength, startHealth, maxHealth, weapon)
        {
            this.Text = new TextTree(Name + ".txt");
            PickNextTimeToMove();
            IsMurderer = IsMurderer;
        }

        public void PickNextTimeToMove()
        {
            var rng = new Random();
            _turnsUntilNextMove = rng.Next(MinTurnsBetweenMoves, MaxTurnsBetweenMoves);
        }

        /// <summary>
        /// Output text and accept player choices until the tree reaches a leaf node
        /// </summary>
        public void Talk(Player player)
        {
            Node currentNode = Text.RootNode;
            while (currentNode != null)
            {
                foreach(string s in currentNode.UnlockedClues)
                {
                    player.Clues.Add(s);
                }
                currentNode = ProcessNode(currentNode);
            }
        }

        public bool IsTimeToMove()
        {
            return _turnsUntilNextMove == 0;
        }

        public void LowerTurnsToNextMove()
        {
            _turnsUntilNextMove--;
        }

        private Node ProcessNode(Node currentNode)
        {
            Console.WriteLine(currentNode.Text);
            List<Node> options = currentNode.AvailableChildren();
            if (options.Count == 0)
            {
                return null;
            }
            Node playerResponse = GetPlayerResponse(currentNode, options);
            Console.WriteLine("> " + playerResponse.Text);
            List<Node> npcResponses = playerResponse.AvailableChildren();
            if (npcResponses.Count == 0)
            {
                return null;
            }
            return npcResponses.First();
        }

        private static Node GetPlayerResponse(Node currentNode, List<Node> options)
        {
            int responseNumber = 1;
            foreach (Node child in options)
            {
                Console.WriteLine(responseNumber + "> " + child.Text);
                ++responseNumber;
            }
            Console.Write("> ");
            int chosenResponse = -1;
            while (Int32.TryParse(Console.ReadLine(), out chosenResponse) == false || chosenResponse < 0 || chosenResponse > currentNode.Children.Count)
            {
                Console.WriteLine("Write a number for one of the responses");
                Console.Write("> ");
            }
            Node playerResponse = options[chosenResponse - 1];
            return playerResponse;
        }
    }
}
