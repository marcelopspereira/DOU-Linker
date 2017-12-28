import re

regexpend = re.compile(r'PRESIDENT')
caput = ""

with open("portos.txt") as f:
    for line in f:
        if not (regexpend.search(line)):
            caput = caput + " " + line
           
        else:
            break

sentences = re.split(';', caput)


def FindAtos():
    num_ato = re.compile(r'([0-9]+(\.[0-9]+)?(\-[0-9]+)?)')
 #   termo = re.compile(r'(Leis)')
    s = " ".join(qualified_actions)
    s2 = re.sub('de \d{1,2} de \w+ de [0-9]{4,4}','',s)
    findings = num_ato.findall(s2)
    return ([i[0] for i in findings])

def FindAction(action):
    global qualified_actions
    if (action == "revoga"):
        actions = re.compile(r'.*evoga.*')
        qualified_actions = filter(actions.match, sentences)
    if (action == "altera"):
        actions = re.compile(r'.*ltera.*')
        qualified_actions = filter(actions.match, sentences)
    L = FindAtos()
    print((action, L))

FindAction("revoga")
FindAction("altera")
#O que revoga?




#Altera?
