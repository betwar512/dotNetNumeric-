# dotNetNumeric-
1 Introduction
Modern astrophysics tends to deal with large amounts of data which needs to
be manipulated in order to be useful to scientists. Cosmologists tend to use
large data samples and t dierent cosmologies1 to the data to see what gives
the best t. They usually have up to 3 or 4 free parameters which can make the
tting process take a large amount of time for large data sets. What I have done
is used supernova data and tted it with dierent cosmologies. These usually
take a very long time to run since they typically have 3 free parameters, and
I require a reasonably good resolution (parameters range from roughly 0 to 1
with a resolution of 0.01). Monte Carlo sampling would make this process much
faster, and so would a less shitty programming language.
2 Description of modern cosmology
When Einstein published his ground breaking paper on the Generally theory
of Relativity, our understanding of gravity was drastically changed. It was
originally understood as an attractive force described by Newton a few centuries
earlier, but Einstein showed that anything that posses mass will bend space
itself. Space and time are manifestations of the same thing, called space-time.
So if space is adjusted, so is time. Armed with this new theory of gravity, a
man named Alexander Friedmann showed that a static (unchanging) universe
cannot exist. This was the consensus of physicists at the time, they actually
believed that the stars did not move and that the universe was innitely old,
even Einstein believed this. What Friedmann showed is that the universe must
either by contracting or expanding, shrinking or growing in size. But the biggest
twist was yet to come.
It was shown by Edwin Hubble that the universe is indeed expanding at a
roughly linear rate with respect to distance, this is called Hubble's law
v = H0D (1)
where v is the velocity, H0 is Hubble's constant and D is the distance to the
galaxy. What we later discovered in the 1990's was that the universe was not
only expanding, but the expansion was accelerating. This is perfectly analogous
to throwing a ball in the air, only to have it accelerate away from you up higher
into the sky. Clearly this goes against commonly held belief that what goes
up must come down. Cosmologists were puzzled by what could be causing the
universe to expand and still have no idea what this stu is, so we call it Dark
Energy.
1This word refers to the dierent models we have of the universe. The main contender is
CDM which stands for Lambda Cold Dark Matter. It isn't important to know the details
of the cosmology, only to understand what it is we are trying to t.
1
3 Description of the CDM model
This is the best model we have so far in cosmology, mainly because the other
models require more data since they are very sensitive to slight adjustments in
their parameters. The equation to describe the universe is
H2 = H2
0


M
a3 +

k
a2 + 


(2)
where 
M is the matter density of the universe, 
 is the dark energy
density of the universe,
k is a curvature term which is related to the two density
parameters, a is a scale factor which describes how the universe has changed
shape in the past and how it will in the future, and nally H is the Hubble
parameter. The Hubble constant I mentioned earlier is the Hubble parameter
as it is measured today, so the Hubble parameter changes as the universe ages.
The values for 
M and 
 have been determined to be (0:27; 0:73) respectively.
So this means that roughly 73% of the universe is made of dark energy, while
the remainder is made of dark matter and baryonic matter (the stu we can
actually see, and are made of). Only about 4% of the matter density is baryonic
matter, while the rest is dark matter.
The scale factor describes the expansion of the universe with respect to time.
We dened it to be equal to 1 at present day, and it converges to 0 at the origin
of the universe. It is related to a value known as the redshift, z
a = (1 + z)􀀀1 (3)
The redshift is a measure of how fast an object is moving away from us,
and appears as a Doppler shift (but most of the redshift we measure IS NOT a
Doppler shift, especially for galaxies that are far away). Have a look at this link
for an understanding of a Doppler shift. Go watch that video before you read
on further. The reason why it is not a Doppler shift (despite what that video
may infer) is because it isn't the object physically moving away from us, rather
the space between us and that object is expanding which makes it appear to
move away from us. The space between us and a distant galaxy is expanding,
and is stretching the photons on their journey towards us.
4 Details of the Program
As I mentioned, we are tting a model to a set of data and seeing which param-
eters give the best t using the minimum 2 value2. The 2 is found using the
equation
2 =
X(observed value 􀀀 expected value)2
observed value
